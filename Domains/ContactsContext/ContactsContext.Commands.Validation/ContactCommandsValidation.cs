using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ContactsContext.Commands.Validation
{
    public class ContactCommandValidator : AbstractValidator<ContactCommand>
    {
        public ContactCommandValidator()
        {
            RuleFor(e => e.ContactId).NotEmpty();
        }
    }

    public class UpdateContactPictureValidator : AbstractValidator<UpdateContactPicture>
    {
        public UpdateContactPictureValidator()
        {
            RuleFor(e => e.PicturePath).NotEmpty();
        }
    }

    public class ContactCategoryCommandValidator : AbstractValidator<ContactCategoryCommand>
    {
        public ContactCategoryCommandValidator()
        {
            RuleFor(e => e.CategoryId).NotEmpty();
        }
    }

    public class ContactLabelCommandValidator : AbstractValidator<ContactLabelCommand>
    {
        public ContactLabelCommandValidator()
        {
            RuleFor(e => e.LabelTypeId).NotEmpty();
            RuleFor(e => e.Label).NotEmpty();
        }
    }

    public class UpdateContactLabelValidator : AbstractValidator<UpdateContactLabel>
    {
        public UpdateContactLabelValidator()
        {
            RuleFor(e => e.NewLabel).NotEmpty();
            RuleFor(e => e.NewLabel).Must(((e, label) => string.CompareOrdinal(e.Label, e.NewLabel) != 0));
        }
    }

    public class ContactInfoCommandValidator : AbstractValidator<ContactInfoCommand>
    {
        public ContactInfoCommandValidator()
        {
            RuleFor(e => e.ContactInfoTypeId).NotEmpty();
            RuleFor(e => e.Info).NotEmpty();
        }
    }


    public class UpdateContactInfoValidator : AbstractValidator<UpdateContactInfo>
    {
        public UpdateContactInfoValidator()
        {
            RuleFor(e => e.NewInfo).NotEmpty();
            RuleFor(e => e.NewInfo).Must(((e, label) => string.CompareOrdinal(e.Info, e.NewInfo) != 0));
        }
    }

}
