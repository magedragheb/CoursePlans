using CoursePlans.API.Companies.Entities;
using FluentValidation;

namespace CoursePlans.API.Companies.Endpoints.Validators;

public class CompanyDTOValidator : AbstractValidator<CompanyDTO>
{
    public CompanyDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
    }

}