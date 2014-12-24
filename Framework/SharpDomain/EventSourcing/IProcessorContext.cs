using System;
using Microsoft.Practices.ServiceLocation;
using SharpDomain.Properties;

namespace SharpDomain.EventSourcing
{
    public interface IProcessorContext
    {
        bool TryGet(Type type, out object value, string key);
        bool TrySet(Type type, object value, string key);
        bool TryReset(Type type, string key);
    }

    public static class ExtensionsToProcessorContext
    {

        public static bool TryGet(this IProcessorContext context, Type type, out object value)
        {
            if (context == null) throw new ArgumentNullException("context");
            var result = context.TryGet(type, out value, ProcessorContextKeys.Default);
            return result;
        }
        public static bool TrySet(this IProcessorContext context, Type type, object value)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (value == null) throw new ArgumentNullException("value");
            var result = context.TrySet(type, value, ProcessorContextKeys.Default);
            return result;
        }
        public static bool TryReset(this IProcessorContext context, Type type, object value)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (value == null) throw new ArgumentNullException("value");
            var result = context.TryReset(type, ProcessorContextKeys.Default);
            return result;
        }

        public static bool TryGet<T>(this IProcessorContext context, out T value, string key)
        {
            if (context == null) throw new ArgumentNullException("context");
            object obj;
            var result = context.TryGet(typeof(T), out obj, key);
            if (result && obj is T)
            {
                value = (T) obj;
                return true;
            }
            value = default(T);
            return result;
        }
        public static bool TrySet<T>(this IProcessorContext context, T value, string key)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (ReferenceEquals(value, null)) throw new ArgumentNullException("value");
            var result = context.TrySet(typeof(T), value, key);
            return result;
        }
        public static bool TryReset<T>(this IProcessorContext context, string key)
        {
            if (context == null) throw new ArgumentNullException("context");
            var result = context.TryReset(typeof(T), key);
            return result;
        }
        public static bool TryGet<T>(this IProcessorContext context, out T value)
        {
            return context.TryGet(out value, ProcessorContextKeys.Default);
        }
        public static bool TrySet<T>(this IProcessorContext context, T value)
        {
            return context.TrySet(value, ProcessorContextKeys.Default);
        }
        public static bool TryReset<T>(this IProcessorContext context)
        {
            return context.TryReset<T>(ProcessorContextKeys.Default);
        }

        public static object Get(this IProcessorContext context, Type type, string key)
        {
            if (context == null) throw new ArgumentNullException("context");
            object value;
            if (!context.TryGet(type, out value, key)) 
                throw new ArgumentOutOfRangeException("key", key, string.Format(Resources.KeyNotFoundForGivenType, key, type.FullName));
            return value;
        }
        public static void Set(this IProcessorContext context, Type type, object value, string key)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (value == null) throw new ArgumentNullException("value");
            if (!context.TrySet(type, value, key))
                throw new ArgumentOutOfRangeException("key", key, string.Format(Resources.KeyAlreadySetForGivenType, key, type.FullName));
        }
        public static void Reset(this IProcessorContext context, Type type, string key)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (!context.TryReset(type, key))
                throw new ArgumentOutOfRangeException("key", key, string.Format(Resources.KeyNotFoundForGivenType, key, type.FullName));
        }

        public static T Get<T>(this IProcessorContext context, string key)
        {
            return (T)context.Get(typeof (T), key);
        }
        public static void Set<T>(this IProcessorContext context, T value, string key)
        {
            context.Set(typeof(T), value, key);
        }
        public static void Reset<T>(this IProcessorContext context, string key)
        {
            context.Reset(typeof(T), key);
        }
        public static T Get<T>(this IProcessorContext context)
        {
            return context.Get<T>(ProcessorContextKeys.Default);
        }
        public static void Set<T>(this IProcessorContext context, T value)
        {
            context.Set(value, ProcessorContextKeys.Default);
        }
        public static void Reset<T>(this IProcessorContext context)
        {
            context.Reset<T>(ProcessorContextKeys.Default);
        }

    }
}