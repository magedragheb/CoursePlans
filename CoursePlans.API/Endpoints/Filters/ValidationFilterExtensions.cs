namespace CoursePlans.API.Endpoints.Filters;

public static class ValidationFilterExtensions
{
    public static RouteHandlerBuilder WithValidation<T>(this RouteHandlerBuilder builder) =>
    builder.AddEndpointFilter<ValidationFilter<T>>()
            .ProducesValidationProblem();
}