using System;
using SharpDomain.Properties;

namespace SharpDomain.Processing
{
    public static class ExtensionsToCommandProcessingContext
    {
        public const string DefaultTokenName = "default";

        public static bool TrySet<TToken>(this ICommandProcessingContext context, TToken token, string name)
        {
            return context.TrySet(typeof(TToken), token, DefaultTokenName);
        }
        public static bool TrySet<TToken>(this ICommandProcessingContext context, TToken token)
        {
            return context.TrySet(token, DefaultTokenName);
        }
        public static void Set<TToken>(this ICommandProcessingContext context, TToken token, string name)
        {
            if (!context.TrySet(token, name))
                throw new ArgumentException(string.Format(Resources.TokenIsAlreadySet, name, typeof(TToken).FullName));
        }
        public static void Set<TToken>(this ICommandProcessingContext context, TToken token)
        {
            context.Set(token, DefaultTokenName);
        }


        public static bool TryGet<TToken>(this ICommandProcessingContext context, out TToken token, string name)
        {
            object value;
            if (context.TryGet(typeof (TToken), name, out value))
            {
                token = (TToken) value;
                return true;
            }
            token = default(TToken);
            return false;
        }
        public static bool TryGet<TToken>(this ICommandProcessingContext context, out TToken token)
        {
            return context.TryGet(out token, DefaultTokenName);
        }
        public static TToken Get<TToken>(this ICommandProcessingContext context, string name)
        {
            TToken token;
            if (context.TryGet(out token, name))
                return token;
            throw new ArgumentException(string.Format("{0} token of type {1} is not present", name, typeof(TToken).FullName));
        }
        public static TToken Get<TToken>(this ICommandProcessingContext context)
        {
            return context.Get<TToken>(DefaultTokenName);
        }
    }
}