using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NServiceBus;
using SharpDomain.Commanding;

namespace SharpDomain.NServiceBus.Commanding
{
    public class NSBCommandBus : ICommandBus
    {
        public ISendOnlyBus Bus { get; set; }

        public NSBCommandBus(ISendOnlyBus bus)
        {
            this.Bus = bus;
        }

        public ISendCommandCallback Send(IDomainCommand command, Action<IDictionary<string, string>> configHeaders = null)
        {
            UseConfigHeaders(configHeaders);
            var callback = Bus.Send(command);
            var result = new SendCommandCallback(callback);
            return result;
        }

        public ISendCommandCallback Send<TCommand>(Action<TCommand> initCommand, Action<IDictionary<string, string>> configHeaders = null)
            where TCommand : class, IDomainCommand
        {
            UseConfigHeaders(configHeaders);
            var callback = Bus.Send(initCommand);
            var result = new SendCommandCallback(callback);

            return result;
        }

        private void UseConfigHeaders(Action<IDictionary<string, string>> configHeaders)
        {
            if (configHeaders != null)
            {
                configHeaders(Bus.OutgoingHeaders);
            }
        }

        class SendCommandCallback : ISendCommandCallback
        {
            protected ICallback Callback { get; set; }

            public SendCommandCallback(ICallback callback)
            {
                Callback = callback;
            }

            public Task<int> Register()
            {
                return Callback.Register();
            }

            public Task<T> Register<T>()
            {
                return Callback.Register<T>();
            }

            public Task<T> Register<T>(Func<SendCommandCompletionResult, T> completion)
            {
                if (completion == null) throw new ArgumentNullException("completion");
                return Callback.Register(result => completion(new SendCommandCompletionResult()
                {
                    ErrorCode = result.ErrorCode,
                    Messages = result.Messages,
                    State = result.State,
                }));
            }

            public Task Register(Action<SendCommandCompletionResult> completion)
            {
                if (completion == null) throw new ArgumentNullException("completion");
                return Callback.Register(result => completion(new SendCommandCompletionResult()
                {
                    ErrorCode = result.ErrorCode,
                    Messages = result.Messages,
                    State = result.State,
                }));
            }

            public IAsyncResult Register(AsyncCallback callback, object state)
            {
                return Callback.Register(callback, state);
            }

            public void Register<T>(Action<T> callback)
            {
                Callback.Register(callback);
            }

            public void Register<T>(Action<T> callback, object synchronizer)
            {
                Callback.Register(callback, synchronizer);
            }
        }
    }
}
