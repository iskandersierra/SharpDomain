namespace SharpDomain.AccessControl
{
    public interface IAccessControlService
    {
        IAccessControlResponse RequestAccess(IAccessControlRequest request);
    }
}
