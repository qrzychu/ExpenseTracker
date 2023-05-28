using ExpenseTrackerBackend.Properties.AccessPolicies;
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
            UnauthorizedAccessException _ => TypedResults.Unauthorized(),
            _ => TypedResults.Problem(e.Message)
        };
    }

    public static IQueryable<TEntity> ApplyPolicy<TEntity>(this IQueryable<TEntity> queryable, IAccessPolicy<TEntity> policy, User user)
    {
        return policy.Apply(queryable, user);
    }
}
