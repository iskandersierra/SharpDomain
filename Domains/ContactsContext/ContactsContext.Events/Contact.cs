using System;
using SharpDomain.EventSourcing;

namespace ContactsContext.Events
{
    public interface ContactCreated : IAggregateCreatedEvent
    {
    }
    public interface ContactTitleUpdated : IEvent
    {
        string Title { get; set; }
    }
    public interface ContactPictureUpdated : IEvent
    {
        Guid PictureId { get; set; }
    }
    public interface ContactPictureCleared : IEvent
    {
    }

    public interface ContactCategoryEvent : IEvent
    {
        Guid CategoryId { get; set; }
    }
    public interface ContactCategoryIncluded : ContactCategoryEvent
    {
    }
    public interface ContactCategoryExcluded : ContactCategoryEvent
    {
    }

    public interface ContactLabelEvent : IEvent
    {
        Guid LabelTypeId { get; set; }
        string Label { get; set; }
    }
    public interface ContactLabelIncluded : ContactLabelEvent
    {
    }
    public interface ContactLabelExcluded : ContactLabelEvent
    {
    }
    public interface ContactLabelUpdated : ContactLabelEvent
    {
        string NewLabel { get; set; }
    }

    public interface ContactInfoEvent : IEvent
    {
        Guid ContactInfoTypeId { get; set; }
        string Info { get; set; }
    }
    public interface ContactInfoIncluded : ContactInfoEvent
    {
    }
    public interface ContactInfoExcluded : ContactInfoEvent
    {
    }
    public interface ContactInfoUpdated : ContactInfoEvent
    {
        string NewInfo { get; set; }
    }
}
