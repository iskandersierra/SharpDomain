using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SharpDomain.EventSourcing;
using SharpDomain.Properties;

namespace SharpDomain.Business
{
    /// <summary>
    /// A stateful aggregate is an aggregate which is aware of all ore some of the events applied to it
    /// so it can build a state resulting from the incomming events. This way a command handler can check
    /// more complex logic over the incomming command and the state of the aggregate.
    /// For each event interface TEvent the aggregate wants to handle, a method 
    /// private void Apply(TEvent @event);
    /// must be implemented. 
    /// When versioning events Ev2 -> Ev1, a given stateful aggregate must decide which version of the 
    /// event it is going to handle. It cannot have two apply methods with both versions of the event 
    /// and an exception is thrown when the first aggregate instance is created for the erroneous type.
    /// </summary>
    public abstract class StatefulAggregate : Aggregate
    {
        private const string ApplyMethodsName = "Apply";
        private static readonly Type EventType = typeof(IEvent);
        private static readonly Type StatefulAggregateType = typeof(StatefulAggregate);
        private readonly StatefulApplyMethods _applyMethods;

        protected StatefulAggregate()
        {
            _applyMethods = GetApplyMethods(this.GetType());
        }

        protected override void ApplyOverride(IEvent @event)
        {
            _applyMethods.ApplyEvent(this, @event);
        }

        #region [ Reflection cache registration ]

        private static readonly ConcurrentDictionary<Type, StatefulApplyMethods> ApplyMethodsCache = new ConcurrentDictionary<Type, StatefulApplyMethods>();
        private static readonly ConcurrentDictionary<Type, EventTypeApplyInformation> EventTypeApplyInfoCache = new ConcurrentDictionary<Type, EventTypeApplyInformation>();

        private static StatefulApplyMethods GetApplyMethods(Type type)
        {
            var result = ApplyMethodsCache.GetOrAdd(type, CreateApplyMethods);
            return result;
        }

        private static StatefulApplyMethods CreateApplyMethods(Type type)
        {
            var allMethods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            var methodsCalledApply = allMethods.Where(m => m.Name == ApplyMethodsName);
            var voidMethods = methodsCalledApply.Where(m => m.ReturnType == typeof(void));
            var methodsAndParameters = voidMethods.Select(m => new {Method = m, Parameters = m.GetParameters()});
            var withOneInputParameter = methodsAndParameters.Where(m => m.Parameters.Length == 1 && !m.Parameters[0].IsOut);
            var methodAndEventType = withOneInputParameter.Select(m => new {m.Method, Type = m.Parameters[0].ParameterType});
            var applyMethods = methodAndEventType.Where(m => m.Type.IsInterface && EventType.IsAssignableFrom(m.Type) && m.Type.Assembly != EventType.Assembly).ToArray();

            // Verify no two methods could receive the same event type at the same time
            var conflicting = 
                (from m1 in applyMethods
                from m2 in applyMethods
                where m1 != m2 && (m1.Type.IsAssignableFrom(m2.Type))
                select new { OlderVersionType = m1.Type, NewerVersionType = m2.Type }).ToArray();

            if (conflicting.Any())
                throw new InvalidOperationException(string.Format("Aggregate {0} contains conflicting apply methods for different versions of the same event: {1}", type.FullName, string.Join(", ", conflicting.Select(c => string.Format("{0} --> {1}", c.NewerVersionType.FullName, c.OlderVersionType.FullName)))));

            var actionDictionary = applyMethods
                .ToDictionary(m => m.Type, m => CreateActionFromMethod(type, m.Type, m.Method));

            var result = new StatefulApplyMethods(actionDictionary);
            return result;
        }

        private static Action<StatefulAggregate, IEvent> CreateActionFromMethod(Type aggregateType, Type eventType, MethodInfo method)
        {
            var aggregate = Expression.Parameter(StatefulAggregateType, "aggregate");
            var @event = Expression.Parameter(EventType, "ev");
            var lambda = Expression.Lambda<Action<StatefulAggregate, IEvent>>(
                Expression.Call(
                    Expression.TypeAs(aggregate, aggregateType), 
                    method,
                    Expression.TypeAs(@event, eventType)),
                aggregate, @event);
            var action = lambda.Compile();
            return action;
        }


        class StatefulApplyMethods
        {
            private readonly ReadOnlyDictionary<Type, Action<StatefulAggregate, IEvent>> _applyers;

            public StatefulApplyMethods(Dictionary<Type, Action<StatefulAggregate, IEvent>> applyers)
            {
                if (applyers == null) throw new ArgumentNullException("applyers");
                _applyers = new ReadOnlyDictionary<Type, Action<StatefulAggregate, IEvent>>(applyers);
            }

            public void ApplyEvent(StatefulAggregate instance, IEvent @event)
            {
                var eventType = @event.GetType();
                var interfaces = GetInterfaces(eventType);

                foreach (var @interface in interfaces)
                {
                    Action<StatefulAggregate, IEvent> action;
                    if (_applyers.TryGetValue(@interface, out action))
                    {
                        action(instance, @event);
                        return;
                    }
                }
            }

            private static Type[] GetInterfaces(Type eventType)
            {
                var result = EventTypeApplyInfoCache.GetOrAdd(eventType, CreateInterfaces);
                return result.Interfaces;
            }

            private static EventTypeApplyInformation CreateInterfaces(Type eventType)
            {
                var interfaces = eventType.GetInterfaces()
                    .Where(e => e.Assembly != EventType.Assembly)
                    .ToList();

                if (interfaces.Count == 0)
                {
                    return new EventTypeApplyInformation(
                        () =>
                        {
                            throw new ArgumentException(string.Format(Resources.EventDoNotImplementAnyInterface, eventType.FullName), "event");
                        });
                }

                if (interfaces.Count > 1)
                {
                    Action throwAction = null;

                    // Sort interfaces so that those closer to the event class are considered first
                    // F. ex.: A <- B <- C <- Event should give list [C, B, A]
                    interfaces.Sort((a, b) =>
                    {
                        if (a.IsAssignableFrom(b))
                            return 1; // then a > b so b goes first
                        if (b.IsAssignableFrom(a))
                            return -1; // then a < b so a goes first
                        if (throwAction == null)
                        {
                            throwAction = () =>
                            {
                                throw new ArgumentException(string.Format(Resources.EventImplementNonVersionedInterfaces, eventType.FullName, a.FullName, b.FullName), "event");
                            };
                        }
                        return 0;
                    });

                    if (throwAction != null)
                        return new EventTypeApplyInformation(throwAction);
                }

                return new EventTypeApplyInformation(interfaces.ToArray());
            }
        }

        internal class EventTypeApplyInformation
        {
            private readonly Type[] _interfaces;
            private readonly Action _throwException;

            public EventTypeApplyInformation(Type[] interfaces)
            {
                _interfaces = interfaces;
            }

            public EventTypeApplyInformation(Action throwException)
            {
                _throwException = throwException;
            }

            public Type[] Interfaces
            {
                get
                {
                    if (ThrowException != null)
                        ThrowException();
                    return _interfaces;
                }
            }

            public Action ThrowException
            {
                get { return _throwException; }
            }
        }

        #endregion
    }
}