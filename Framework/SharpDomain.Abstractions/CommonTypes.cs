using System;
using SharpDomain.Commanding;
using SharpDomain.Eventing;
using SharpDomain.Messaging;

namespace SharpDomain
{
    public static class CommonTypes
    {
        public static readonly Type DomainCommand = typeof (IDomainCommand);
        public static readonly Type DomainEvent = typeof (IDomainEvent);
        public static readonly Type DomainMessage = typeof (IDomainMessage);
    }
}
