using System;
using System.ComponentModel;
using Common.Logging;
using ContactsDomain.Commands;
using SharpDomain.Client;
using SharpDomain.Commanding;

namespace ContactsDomain.Client
{
    [Category("Contact")]
    public class ContactCommandProvider : ClientCommandProvider
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        //public ContactCommandProvider(ICommandBus bus)
        //{
        //    Bus = bus;
        //}

        //public ICommandBus Bus { get; set; }

        [DisplayName("Creates a contact")]
        [Description("Creates a new contact with given title")]
        public void Create([Description("contact title")]string title)
        {
            Log.TraceFormat(string.Format("Create contact '{0}'", title));
            //Bus.Send<CreateContact>(c => { c.Title = title; });
        }

        [Description("Updates contact title")]
        public void UpdateTitle(Guid contactId, string title)
        {
            Log.TraceFormat(string.Format("Update contact title '{0}' with id {1}", title, contactId));
            //Bus.Send<UpdateContactTitle>(c => { c.AggregateId = contactId; c.Title = title; });
        }
    }
}
