using System;
using SharpDomain.EventSourcing;

namespace ContactsContext.Commands
{
    public interface CategoryCommand : ICommand
    {
        Guid CategoryId { get; set; }
    }
    public interface CreateCategory : CategoryCommand, ICreateAggregateCommand
    {
        string Title { get; set; }
        bool IsExternal { get; set; }
    }
    public interface UpdateCategoryTitle : CategoryCommand
    {
        string Title { get; set; }
    }
    public interface DeleteCategory : CategoryCommand
    {
    }
}