using ExpenseTrackerBackend.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerBackend.Expenses;

public static class ExpensesApi
{
    public static void MapExpensesApi(this WebApplication app)
    {
        app.MapGet("/expenses", async (IMediator mediator) => await mediator.Send(new GetExpenses()))
            .WithName("GetExpenses");

        app.MapGet("expenses/{id:int}", async (IMediator mediator, int id) =>
            {
                var expense = await mediator.Send(new GetExpense(id));
                return expense is null ? (IResult)TypedResults.NotFound() : TypedResults.Ok(expense);
            })
            .WithName("GetExpense");

        app.MapPost("/expenses", async (IMediator mediator, AddExpense addExpense) =>
            {
                return (await mediator.Send(addExpense)).Match(
                    id => (IResult)TypedResults.Created($"expenses/{id}", id),
                    Utils.MapExceptionToResult);
            })
            .WithName("AddExpense");

        app.MapPut("/expenses", async (IMediator mediator, [FromBody] UpdateExpense updateExpense) =>
            {
                return (await mediator.Send(updateExpense)).Match(
                    i => (IResult)TypedResults.Ok(i),
                    Utils.MapExceptionToResult);
            })
            .WithName("UpdateExpense");

        app.MapDelete("/delete/{id:int}", async (IMediator mediator, int id) =>
        {
            var result = await mediator.Send(new DeleteExpense(id));
            return result ? (IResult)TypedResults.Ok() : TypedResults.NotFound();
        }).WithName("DeleteExpense");
    }
}