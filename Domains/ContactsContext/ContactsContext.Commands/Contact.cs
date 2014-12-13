using System;
using SharpDomain.EventSourcing;

namespace ContactsContext.Commands
{
    public interface CreateContact : ICreateAggregateCommand
    {
        Guid ContactId { get; set; }
        string Title { get; set; }
    }
    public interface UpdateContactTitle : ICommand
    {
        Guid ContactId { get; set; }
        string Title { get; set; }
    }
    public interface UpdateContactPicture : ICommand
    {
        Guid ContactId { get; set; }
        Guid PictureId { get; set; }
    }
    public interface ClearContactPicture : ICommand
    {
        Guid ContactId { get; set; }
    }

    public interface IncludeContactCategory : ICommand
    {
        Guid ContactId { get; set; }
        Guid CategoryId { get; set; }
    }
    public interface ExcludeContactCategory : ICommand
    {
        Guid ContactId { get; set; }
        Guid CategoryId { get; set; }
    }

    public interface IncludeContactLabel : ICommand
    {
        Guid ContactId { get; set; }
        Guid LabelTypeId { get; set; }
        string Label { get; set; }
    }
    public interface ExcludeContactLabel : ICommand
    {
        Guid ContactId { get; set; }
        Guid LabelTypeId { get; set; }
        string Label { get; set; }
    }
    public interface UpdateContactLabel : ICommand
    {
        Guid ContactId { get; set; }
        Guid LabelTypeId { get; set; }
        string CurrentLabel { get; set; }
        string NewLabel { get; set; }
    }

    public interface IncludeContactInfo : ICommand
    {
        Guid ContactId { get; set; }
        Guid ContactInfoTypeId { get; set; }
        string Info { get; set; }
    }
    public interface ExcludeContactInfo : ICommand
    {
        Guid ContactId { get; set; }
        Guid ContactInfoTypeId { get; set; }
        string Info { get; set; }
    }
    public interface UpdateContactInfo : ICommand
    {
        Guid ContactId { get; set; }
        Guid ContactInfoTypeId { get; set; }
        string CurrentInfo { get; set; }
        string NewInfo { get; set; }
    }

}
