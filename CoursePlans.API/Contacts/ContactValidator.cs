using CoursePlans.API.Contacts.Entities;
using FluentValidation;

namespace CoursePlans.API.Contacts.Endpoints.Validators;

public class ContactDTOValidator : AbstractValidator<ContactDTO>
{
    public ContactDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}