using System;
using SharpDomain.Messaging;

namespace SharpDomain.CoreDomains.SystemAccessing.UserDomain.Commands
{
    public class CreateUserCommand : Command<Guid>, CreateUser
    {
        private readonly string _userName;
        private readonly string _password;

        public CreateUserCommand(Guid id, string userName, string password) : base(id)
        {
            _userName = userName;
            _password = password;
        }

        public string UserName
        {
            get { return _userName; }
        }

        public string Password
        {
            get { return _password; }
        }
    }
}
