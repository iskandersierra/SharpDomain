using System;
using SharpDomain.EventSourcing;

namespace ContactsContext.Commands
{
    public interface ContactInfoTypeCommand : ICommand
    {
        Guid ContactInfoTypeId { get; set; }
    }
    public interface CreateContactInfoType : ContactInfoTypeCommand, ICreateAggregateCommand
    {
        string Title { get; set; }
        bool IsExternal { get; set; }
    }
    public interface UpdateContactInfoTypeTitle : ContactInfoTypeCommand
    {
        string Title { get; set; }
    }
    public interface DeleteContactInfoType : ContactInfoTypeCommand
    {
    }
}