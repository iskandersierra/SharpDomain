using System;

namespace SharpDomain.EventSourcing
{
    public interface ICommandHandlerContext
    {
        void Emmit<T>(Action<T> action) where T : class;
    }

    public static class ExtensionsOfCommandHandlerContext
    {
        public static void Emmit<T>(this ICommandHandlerContext context) where T : class
        {
            context.Emmit<T>(e => { });
        }
    }
}