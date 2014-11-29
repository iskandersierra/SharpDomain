using System;
using SharpDomain.Aggregates;
using SharpDomain.CoreDomains.SystemAccessing.UserDomain.Events;
using SharpDomain.CoreDomains.SystemAccessing.UserDomain.Factories;

namespace SharpDomain.CoreDomains.SystemAccessingImplementation.UserDomain.Factories
{
    internal class UserAggregate : Aggregate<Guid>, User
    {
        internal UserAggregate(Guid id)
            : base(id)
        {
            Handles<UserCreated>(OnCreated);
        }

        public string UserName { get; private set; }

        public string Password { get; private set; }

        public string Email { get; private set; }


        private void OnCreated(UserCreated ev)
        {
            UserName = ev.UserName;
            Password = ev.Password;
            Email = ev.Email;
        }
    }
}