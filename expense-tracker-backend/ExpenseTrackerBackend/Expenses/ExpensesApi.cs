using ExpenseTrackerBackend.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerBackend.Expenses;

public static class ExpensesApi
{
    public static void MapExpensesApi(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("/expenses")
            .RequireAuthorization();

        groupBuilder.MapGet("/", async (IMediator mediator) => await mediator.Send(new GetExpenses()))
            .WithName("GetExpenses");

        groupBuilder.MapGet("/{id:int}", async (IMediator mediator, int id) =>
            {
                var expense = await mediator.Send(new GetExpense(id));
                return expense is null ? (IResult)TypedResults.NotFound() : TypedResults.Ok(expense);
            })
            .WithName("GetExpense");

        groupBuilder.MapPost("/", async (IMediator mediator, AddExpense addExpense) =>
            {
                return (await mediator.Send(addExpense)).Match(
                    id => TypedResults.Created($"expenses/{id}", id),
                    Utils.MapExceptionToResult);
            })
            .WithName("AddExpense");

        groupBuilder.MapPut("/", async (IMediator mediator, [FromBody] UpdateExpense updateExpense) =>
            {
                return (await mediator.Send(updateExpense)).Match(
                    i => TypedResults.Ok(i),
                    Utils.MapExceptionToResult);
            })
            .WithName("UpdateExpense");

        groupBuilder.MapDelete("/{id:int}", async (IMediator mediator, int id) =>
        {
            var result = await mediator.Send(new DeleteExpense(id));
            return result ? (IResult)TypedResults.Ok() : TypedResults.NotFound();
        }).WithName("DeleteExpense");
    }
}