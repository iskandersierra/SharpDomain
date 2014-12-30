namespace SharpDomain.Client
{
    public interface ICommandParameterInfo : ICommandItemInfo
    {
        bool IsOptional { get; }

        object Convert(string value);
    }
}