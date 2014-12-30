namespace SharpDomain.Commanding
{
    public class SendCommandCompletionResult
    {
        public int ErrorCode { get; set; }
        public object[] Messages { get; set; }
        public object State { get; set; }
    }
}