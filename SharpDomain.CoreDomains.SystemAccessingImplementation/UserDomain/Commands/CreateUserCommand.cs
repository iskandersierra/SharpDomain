using System;
using SharpDomain.CoreDomains.SystemAccessing.UserDomain.Commands;
using SharpDomain.Messaging;

namespace SharpDomain.CoreDomains.SystemAccessingImplementation.UserDomain.Commands
{
    public class CreateUserCommand : Command<Guid>, CreateUser
    {
        private readonly string _userName;
        private readonly string _password;
        private readonly string _email;

        public CreateUserCommand(Guid id, string userName, string password, string email) : base(id)
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
