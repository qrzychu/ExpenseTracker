using ExpenseTrackerBackend.Data.DTO;
using ExpenseTrackerBackend.Expenses;
using ExpenseTrackerBackend.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerBackend.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ExpensesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpensesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetExpenses")]
    [Produces(typeof(IEnumerable<ExpenseDto>))]
    public async Task<IResult> GetExpenses()
    {
        return TypedResults.Ok(await _mediator.Send(new GetExpenses()));
    }
    
    [HttpGet("{id:int}", Name = "GetExpense")]
    [Produces(typeof(ExpenseDto))]
    public async Task<IResult> GetExpense(int id)
    {
        var expense = await _mediator.Send(new GetExpense(id));
        return expense is null ? (IResult)TypedResults.NotFound() : TypedResults.Ok(expense);
    }
    
    [HttpPost(Name = "AddExpense")]
    [Produces(typeof(int))]
    public async Task<IResult> AddExpense([FromBody] AddExpense addExpense)
    {
        return (await _mediator.Send(addExpense)).Match(
            id => TypedResults.Created($"expenses/{id}", id),
            Utils.MapExceptionToResult);
    }
    
    [HttpPut(Name = "UpdateExpense")]
    [Produces(typeof(int))]
    public async Task<IResult> UpdateExpense([FromBody] UpdateExpense updateExpense)
    {
        return (await _mediator.Send(updateExpense)).Match(
            i => TypedResults.Ok(i),
            Utils.MapExceptionToResult);
    }
    
    [HttpDelete("{id:int}", Name = "DeleteExpense")]
    public async Task<IResult> DeleteExpense(int id)
    {
        var result = await _mediator.Send(new DeleteExpense(id));
        return result ? (IResult)TypedResults.Ok() : TypedResults.NotFound();
    }
}