using ExpenseTrackerBackend.AccessPolicies;
using ExpenseTrackerBackend.Data.DTO;
using ExpenseTrackerBackend.Infrastructure;
using ExpenseTrackerBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerBackend.Expenses;

public class GetExpenseHandler : IRequestHandler<GetExpense, ExpenseDto?>
{
    private readonly ExpenseTrackerDbContext _db;
    private readonly ICurrentUserService _currentUser;

    public GetExpenseHandler(ExpenseTrackerDbContext db, ICurrentUserService currentUser)
    {
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<ExpenseDto?> Handle(GetExpense request, CancellationToken cancellationToken)
    {
        var result = await _db.Expenses
            .AsNoTracking()
            .Include(x => x.ExpenseType)
            .ApplyPolicy(new OwnerCanAccessOwnExpenses(), await _currentUser.GetCurrentUser())
            .FirstOrDefaultAsync(x => x.Id ==  request.Id, cancellationToken);
        
        return result?.MapToDto();
    }
}

public record struct GetExpense(int Id) : IRequest<ExpenseDto?>;