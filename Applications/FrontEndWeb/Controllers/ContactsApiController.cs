using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ContactsContext.ReadModel;

namespace FrontEndWeb.Controllers
{
    public class ContactsApiController : ApiController
    {
        public IEnumerable<ContactInfo> Get()
        {
            return new[]
            {
                new ContactInfo{ ContactId = Guid.NewGuid(), Code = "iskAB4F", Title = "Iskander Sierra"}, 
                new ContactInfo{ ContactId = Guid.NewGuid(), Code = "mvmK2T0", Title = "Mario del Valle"}, 
            };
        }
    }
}
