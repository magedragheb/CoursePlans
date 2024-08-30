using CoursePlans.API.Plans.Entities;
using FluentValidation;

namespace CoursePlans.API.Plans.Endpoints.Validators;

public class PlanDTOValidator : AbstractValidator<PlanDTO>
{
    public PlanDTOValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.CourseId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate).NotEmpty().GreaterThanOrEqualTo(x => x.StartDate);
    }
}