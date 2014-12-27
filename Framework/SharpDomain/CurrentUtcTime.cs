using System;

namespace SharpDomain
{
    public class CurrentUtcTime : ICurrentTime
    {
        public DateTime Now()
        {
            return DateTime.UtcNow;
        }
    }
}