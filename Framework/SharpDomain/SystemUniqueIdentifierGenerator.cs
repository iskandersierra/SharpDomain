using System;

namespace SharpDomain
{
    public class SystemUniqueIdentifierGenerator : IUniqueIdentifierGenerator
    {
        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}