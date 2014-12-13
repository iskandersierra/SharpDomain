using System;

namespace ContactsContext.Events
{
    public interface ContactCreated
    {
        Guid ContactId { get; set; }
    }
    public interface ContactTitleUpdated
    {
        Guid ContactId { get; set; }
        string Title { get; set; }
    }
}
