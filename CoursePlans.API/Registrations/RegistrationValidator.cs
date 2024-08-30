using CoursePlans.API.Registrations.Entities;
using FluentValidation;

namespace CoursePlans.API.Registrations.Validators;

public class RegistrationDTOValidator : AbstractValidator<RegistrationDTO>
{
    public RegistrationDTOValidator()
    {
        RuleFor(x => x.PlanId).NotEmpty();
        RuleFor(x => x.CourseId).NotEmpty();
        RuleFor(x => x.CompanyId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Price).NotEmpty();
        RuleFor(x => x.Date).NotEmpty();
    }
}