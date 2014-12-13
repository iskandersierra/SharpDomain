using System;
using SharpDomain.EventSourcing;

namespace ContactsContext.Commands
{
    public interface CreateCategory : ICreateAggregateCommand
    {
        Guid CategoryId { get; set; }
        string Title { get; set; }
        bool IsExternal { get; set; }
    }
    public interface UpdateCategoryTitle : ICommand
    {
        Guid CategoryId { get; set; }
        string Title { get; set; }
    }
    public interface DeleteCategory : ICommand
    {
        Guid CategoryId { get; set; }
    }
}