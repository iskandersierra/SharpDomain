using System;
using SharpDomain.Aggregates;
using SharpDomain.CoreDomains.SystemAccessing.UserDomain.Commands;
using SharpDomain.CoreDomains.SystemAccessing.UserDomain.Factories;
using SharpDomain.CoreDomains.SystemAccessingImplementation.UserDomain.Events;

namespace SharpDomain.CoreDomains.SystemAccessingImplementation.UserDomain.Factories
{
    public class UserAggregateFactory : UserFactory
    {
        public UserAggregateFactory(
            INewUserIdGenerator newUserIdGenerator)
        {
            NewUserIdGenerator = newUserIdGenerator;
        }

        private INewUserIdGenerator NewUserIdGenerator { get; set; }

        public User CreateUser(CreateUser command)
        {
            User user = new UserAggregate(NewUserIdGenerator.NewId());

            user.AppendEvent(new UserCreatedEvent(command.UserName, command.Password, command.Email));

            return user;
        }
    }
}
