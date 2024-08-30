using FluentValidation;

namespace CoursePlans.API.Endpoints.Filters;

public class ValidationFilter<T>(IValidator<T> validator) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var request = context.Arguments.OfType<T>().FirstOrDefault();
        if (request is null) return null;
        var result = await validator.ValidateAsync(request, context.HttpContext.RequestAborted);
        if (!result.IsValid) return TypedResults.ValidationProblem(result.ToDictionary());
        return await next(context);
    }
}