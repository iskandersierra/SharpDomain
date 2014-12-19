using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDomain.DDD;

namespace ContactsContext
{
    public class ContactsContext : BoundedContext
    {
        public ContactsContext()
        {
            //HasDomainObject<Contact>();
            HasDomainObjectsInThisAssembly();
        }
    }

    public class Contact : DomainObject
    {
        
    }

    public class ContactType : DomainObject
    {
        
    }

    public class ContactLabelType : DomainObject
    {
        
    }

    public class ContactInfoType : DomainObject
    {
        
    }

    public class ContactExchangeType : DomainObject
    {
        
    }
}
