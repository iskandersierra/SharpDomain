using System;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;

namespace SharpDomain.EventSourcing
{
    public class ProcessorContext : IProcessorContext
    {
        private Dictionary<Type, Dictionary<string, object>> _dictionary;

        public ProcessorContext()
        {
        }

        public bool TryGet(Type type, out object value, string key)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (key == null) throw new ArgumentNullException("key");

            value = null;
            if (_dictionary == null)
                return false;

            Dictionary<string, object> values;
            if (!_dictionary.TryGetValue(type, out values))
                return false;

            if (!values.TryGetValue(key, out value))
                return false;

            return true;
        }

        public bool TrySet(Type type, object value, string key)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (value == null) throw new ArgumentNullException("value");
            if (key == null) throw new ArgumentNullException("key");

            if (_dictionary == null)
                _dictionary = new Dictionary<Type, Dictionary<string, object>>();

            Dictionary<string, object> values;
            if (!_dictionary.TryGetValue(type, out values))
            {
                values = new Dictionary<string, object>();
                _dictionary[type] = values;
            }

            if (values.ContainsKey(key))
                return false;

            values[key] = value;
            
            return true;
        }

        public bool TryReset(Type type, string key)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (key == null) throw new ArgumentNullException("key");

            if (_dictionary == null)
                return false;

            Dictionary<string, object> values;
            if (!_dictionary.TryGetValue(type, out values))
                return false;

            if (!values.Remove(key))
                return false;

            return true;
        }
    }
}