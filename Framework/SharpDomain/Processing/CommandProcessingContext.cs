using System;
using System.Collections.Generic;
using SharpDomain.Messaging;
using SharpDomain.Properties;

namespace SharpDomain.Processing
{
    /// <summary>
    /// Allows communication between command processing steps in the processing pipeline
    /// </summary>
    public class CommandProcessingContext : ICommandProcessingContext
    {
        private readonly ICommand _command;
        private Dictionary<Type, Dictionary<string, object>> _tokens;
        private CommandProcessingError _error;

        public CommandProcessingContext(ICommand command)
        {
            _command = command;
        }

        public ICommand Command
        {
            get { return _command; }
        }

        public CommandProcessingError Error
        {
            get { return _error; }
        }

        public void SetError(CommandProcessingError error)
        {
            if (error == null) throw new ArgumentNullException("error");
            if (_error != null) throw new InvalidOperationException(Resources.AnErrorIsAlreadyRegistered);
            _error = error;
        }

        public bool TrySet(Type tokenType, object token, string name)
        {
            if (_tokens == null)
                _tokens = new Dictionary<Type, Dictionary<string, object>>();
            
            Dictionary<string, object> names;
            if (!_tokens.TryGetValue(tokenType, out names))
            {
                names = new Dictionary<string, object>();
                _tokens.Add(tokenType, names);
            }

            object value;
            if (names.TryGetValue(name, out value))
            {
                return false;
            }

            names.Add(name, token);
            return true;
        }

        public bool TryGet(Type tokenType, string name, out object token)
        {
            if (_tokens == null)
            {
                token = null;
                return false;
            }
            
            Dictionary<string, object> names;
            if (!_tokens.TryGetValue(tokenType, out names))
            {
                token = null;
                return false;
            }

            var result = names.TryGetValue(name, out token);

            return result;
        }
    }
}