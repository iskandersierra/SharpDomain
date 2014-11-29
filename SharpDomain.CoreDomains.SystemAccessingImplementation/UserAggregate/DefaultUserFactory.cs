using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDomain.CoreDomains.SystemAccessing.UserAggregate;

namespace SharpDomain.CoreDomains.SystemAccessingImplementation.UserAggregate
{
    public class EventSourcedUserFactory : UserFactory
    {
        public User Create(string username, string password, string email)
        {
            var user = new EventSourcedUser();

            user.Created(username, password, email);

            return user;
        }
    }
}
