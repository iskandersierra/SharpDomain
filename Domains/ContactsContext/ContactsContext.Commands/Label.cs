using System;
using SharpDomain.EventSourcing;

namespace ContactsContext.Commands
{
    public interface CreateLabelType : ICreateAggregateCommand
    {
        Guid LabelTypeId { get; set; }
        string Title { get; set; }
        bool IsExternal { get; set; }
    }
    public interface UpdateLabelTypeTitle : ICommand
    {
        Guid LabelTypeId { get; set; }
        string Title { get; set; }
    }
    public interface DeleteLabelType : ICommand
    {
        Guid LabelTypeId { get; set; }
    }
}