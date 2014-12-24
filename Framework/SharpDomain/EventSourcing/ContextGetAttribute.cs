using System;

namespace SharpDomain.EventSourcing
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
    public sealed class ContextGetAttribute : Attribute
    {
        public ContextGetAttribute() : this(ProcessorContextKeys.Default)
        {
        }

        public ContextGetAttribute(string key)
        {
            Key = key;
        }

        public string Key { get; private set; }
    }
}