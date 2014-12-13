using System;
using SharpDomain.EventSourcing;

namespace ContactsContext.Commands
{
    public interface LabelTypeCommand : ICommand
    {
        Guid LabelTypeId { get; set; }
    }
    public interface CreateLabelType : LabelTypeCommand, ICreateAggregateCommand
    {
        string Title { get; set; }
        bool IsExternal { get; set; }
    }
    public interface UpdateLabelTypeTitle : LabelTypeCommand
    {
        string Title { get; set; }
    }
    public interface DeleteLabelType : LabelTypeCommand
    {
    }
}