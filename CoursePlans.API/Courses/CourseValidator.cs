using CoursePlans.API.Courses.Entities;
using FluentValidation;

namespace CoursePlans.API.Courses.Endpoints.Validators;

public class CourseDTOValidator : AbstractValidator<CourseDTO>
{
    public CourseDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.Code)
            .NotEmpty();
    }
}