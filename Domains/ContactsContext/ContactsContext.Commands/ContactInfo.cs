using System;
using SharpDomain.EventSourcing;

namespace ContactsContext.Commands
{
    public interface CreateContactInfoType : ICreateAggregateCommand
    {
        Guid ContactInfoTypeId { get; set; }
        string Title { get; set; }
        bool IsExternal { get; set; }
    }
    public interface UpdateContactInfoTypeTitle : ICommand
    {
        Guid ContactInfoTypeId { get; set; }
        string Title { get; set; }
    }
    public interface DeleteContactInfoType : ICommand
    {
        Guid ContactInfoTypeId { get; set; }
    }
}