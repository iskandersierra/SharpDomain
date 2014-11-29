using System;
using SharpDomain.Messaging;

namespace SharpDomain.CoreDomains.SystemAccessing.UserDomain.Events
{
    public class UserCreatedEvent : Event<Guid>, UserCreated
    {
        private readonly string _userName;
        private readonly string _password;
        private readonly string _email;

        public UserCreatedEvent(string userName, string password, string email)
        {
            _userName = userName;
            _password = password;
            _email = email;
        }

        public string UserName
        {
            get { return _userName; }
        }

        public string Password
        {
            get { return _password; }
        }

        public string Email
        {
            get { return _email; }
        }
    }
}
