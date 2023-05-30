using ExpenseTrackerBackend.Data.DTO;
using ExpenseTrackerBackend.ExpenseTypes;
using ExpenseTrackerBackend.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerBackend.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ExpenseTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpenseTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet(Name = "GetExpenseTypes")]
    [Produces(typeof(IEnumerable<ExpenseTypeDto>))]
    public async Task<IResult> GetExpenseTypes()
    {
        var expenseTypes = await _mediator.Send(new GetExpenseTypes());
        return TypedResults.Ok(expenseTypes.Select(x => x.MapToDto()));
    }
    
    [HttpPost(Name = "AddExpenseType")]
    public async Task<IResult> AddExpenseType([FromBody] AddExpenseType addExpenseType)
    {
        return (await _mediator.Send(addExpenseType)).Match(
            id => TypedResults.Created($"expense-types", id),
            e => e.MapExceptionToResult());
    }
    
    [HttpDelete("{id:int}", Name = "DeleteExpenseType")]
    public async Task<IResult> DeleteExpenseType(int id)
    {
        var result = await _mediator.Send(new DeleteExpenseType(id));
        return result ? TypedResults.Ok() : TypedResults.NotFound();
    }
}