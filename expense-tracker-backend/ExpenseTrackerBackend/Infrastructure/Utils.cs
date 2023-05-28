using FluentValidation;

namespace ExpenseTrackerBackend.Infrastructure;

public static class Utils
{
    public static IResult MapExceptionToResult(this Exception e)
    {
        return e switch
        {
            ValidationException validationException => TypedResults.ValidationProblem(
                validationException.Errors.ToDictionary(x => x.PropertyName,
                    x => new[] { x.ErrorMessage, "Attempted value: " + x.AttemptedValue })
            ),
            _ => TypedResults.Problem(e.Message)
        };
    }
}