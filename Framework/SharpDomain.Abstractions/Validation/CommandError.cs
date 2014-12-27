namespace SharpDomain.Validation
{
    public class CommandError
    {
        public CommandErrorTarget Target { get; set; }

        public string PropertyName { get; set; }

        public string ErrorMessage { get; set; }

        public string Advice { get; set; }
    }
}
