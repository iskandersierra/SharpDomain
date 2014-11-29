using System;
using Newtonsoft.Json;

namespace SharpDomain.Messaging
{
    public abstract class Envelope
    {
        public static Envelope<T> Create<T>(T body)
        {
            return new Envelope<T>(body);
        }
    }

    public class Envelope<TBody> : Envelope
    {
        public Envelope(TBody body)
        {
            Body = body;
        }

        public TBody Body { get; private set; }

        public static implicit operator Envelope<TBody>(TBody body)
        {
            return new Envelope<TBody>(body);
        }

        public static implicit operator TBody(Envelope<TBody> envelope)
        {
            if (envelope == null) throw new ArgumentNullException("envelope");
            return envelope.Body;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", typeof(TBody).FullName, JsonConvert.SerializeObject(Body, Formatting.None));
        }
    }
}