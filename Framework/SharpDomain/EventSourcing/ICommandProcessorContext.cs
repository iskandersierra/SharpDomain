using System;

namespace SharpDomain.EventSourcing
{
    public interface ICommandProcessorContext : IMessageProcessorContext
    {
        void Emmit<T>(Action<T> action) 
            where T : class, IEvent;
    }

    public static class ExtensionsOfCommandProcessorContext
    {
        public static void Emmit<T>(this ICommandProcessorContext context)
            where T : class, IEvent
        {
            if (context == null) throw new ArgumentNullException("context");
            context.Emmit<T>(@event => { });
        }
    }
}