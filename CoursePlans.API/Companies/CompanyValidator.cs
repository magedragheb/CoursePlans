using CoursePlans.API.Companies.Entities;
using FluentValidation;

namespace CoursePlans.API.Companies.Endpoints.Validators;

public class CompanyValidator : AbstractValidator<Company>
{
    public CompanyValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(50);
    }
}