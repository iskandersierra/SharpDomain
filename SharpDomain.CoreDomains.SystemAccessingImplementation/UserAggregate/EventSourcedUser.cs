using SharpDomain.CoreDomains.SystemAccessing.UserAggregate;

namespace SharpDomain.CoreDomains.SystemAccessingImplementation.UserAggregate
{
    internal class EventSourcedUser : User
    {
        public EventSourcedUser()
        {
            IsNew = true;
        }

        public bool IsNew { get; private set; }

        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }

        public void Created(string username, string password, string email)
        {
            UserName = username;
            Password = password;
            Email = email;
        }
    }
}