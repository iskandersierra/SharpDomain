using System;
using System.Threading.Tasks;

namespace SharpDomain.Commanding
{
    public interface ISendCommandCallback
    {
        Task<int> Register();
        Task<T> Register<T>();
        Task<T> Register<T>(Func<SendCommandCompletionResult, T> completion);
        Task Register(Action<SendCommandCompletionResult> completion);
        IAsyncResult Register(AsyncCallback callback, object state);
        void Register<T>(Action<T> callback);
        void Register<T>(Action<T> callback, object synchronizer);
    }
}