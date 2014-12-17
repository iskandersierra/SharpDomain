using System;
using SharpDomain.EventSourcing;

namespace ContactsContext.Commands
{
    public interface ContactCommand : ICommand
    {
        Guid ContactId { get; set; }
    }
    
    public interface CreateContact : ContactCommand, ICreateAggregateCommand
    {
        string Title { get; set; }
    }

    // Testing for rebus, remove later
    public class CreateContactCommand : CreateContact
    {
        public Guid ContactId { get; set; }
        public string Title { get; set; }
    }

    public interface UpdateContactTitle : ContactCommand
    {
        string Title { get; set; }
    }
    public interface UpdateContactPicture : ContactCommand
    {
        Guid PictureId { get; set; }
    }
    public interface ClearContactPicture : ContactCommand
    {
    }

    public interface ContactCategoryCommand : ContactCommand
    {
        Guid CategoryId { get; set; }
    }
    public interface IncludeContactCategory : ContactCategoryCommand
    {
    }
    public interface ExcludeContactCategory : ContactCategoryCommand
    {
    }

    public interface ContactLabelCommand : ContactCommand
    {
        Guid LabelTypeId { get; set; }
        string Label { get; set; }
    }
    public interface IncludeContactLabel : ContactLabelCommand
    {
    }
    public interface ExcludeContactLabel : ContactLabelCommand
    {
    }
    public interface UpdateContactLabel : ContactLabelCommand
    {
        string NewLabel { get; set; }
    }

    public interface ContactInfoCommand : ContactCommand
    {
        Guid ContactInfoTypeId { get; set; }
        string Info { get; set; }
    }
    public interface IncludeContactInfo : ContactInfoCommand
    {
    }
    public interface ExcludeContactInfo : ContactInfoCommand
    {
    }
    public interface UpdateContactInfo : ContactInfoCommand
    {
        string NewInfo { get; set; }
    }
}
