using System;

namespace SharpDomain.EventSourcing
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class ContextSetAttribute : Attribute
    {
        public ContextSetAttribute(Type type) : this(type, ProcessorContextKeys.Default)
        {
        }

        public ContextSetAttribute(Type type, string key)
        {
            Type = type;
            Key = key;
        }

        public Type Type { get; private set; }
        public string Key { get; private set; }
    }
}