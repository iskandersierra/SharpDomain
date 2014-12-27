using System;

namespace SharpDomain
{
    public class CurrentLocalTime : ICurrentTime
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}