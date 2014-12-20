using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using ContactsContext.ReadModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace FrontEndWeb.Controllers
{
    public class ContactsApiController : ApiController
    {
        public DataSourceResult Get([ModelBinder(typeof(WebApiDataSourceRequestModelBinder))] DataSourceRequest request)
        {
            return GetContactInfos().ToDataSourceResult(request);
        }

        //public IEnumerable<ContactInfo> Get()
        //{
        //    return GetContactInfos();
        //}

        private static IEnumerable<ContactInfo> GetContactInfos()
        {
            return new[]
            {
                new ContactInfo {ContactId = Guid.NewGuid(), Code = "iskAB4F", Title = "Iskander Sierra"},
                new ContactInfo {ContactId = Guid.NewGuid(), Code = "mvmK2T0", Title = "Mario del Valle"},
                new ContactInfo {ContactId = Guid.NewGuid(), Code = "avmK2T0", Title = "Mario A"},
                new ContactInfo {ContactId = Guid.NewGuid(), Code = "bvmK2T0", Title = "Mario Q"},
                new ContactInfo {ContactId = Guid.NewGuid(), Code = "dvmK2T0", Title = "Mario W"},
                new ContactInfo {ContactId = Guid.NewGuid(), Code = "evmK2T0", Title = "Mario E"},
                new ContactInfo {ContactId = Guid.NewGuid(), Code = "fvmK2T0", Title = "Mario R"},
                new ContactInfo {ContactId = Guid.NewGuid(), Code = "gvmK2T0", Title = "Mario T"},
                new ContactInfo {ContactId = Guid.NewGuid(), Code = "hvmK2T0", Title = "Mario Y"},
                new ContactInfo {ContactId = Guid.NewGuid(), Code = "jvmK2T0", Title = "Mario U"},
                new ContactInfo {ContactId = Guid.NewGuid(), Code = "kvmK2T0", Title = "Mario I"},
                new ContactInfo {ContactId = Guid.NewGuid(), Code = "lvmK2T0", Title = "Mario O"},
            };
        }
    }
}
