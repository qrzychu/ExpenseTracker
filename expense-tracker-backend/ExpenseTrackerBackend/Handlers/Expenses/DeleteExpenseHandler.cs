using ExpenseTrackerBackend.AccessPolicies;
using ExpenseTrackerBackend.Infrastructure;
using ExpenseTrackerBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerBackend.Expenses;

public class DeleteExpenseHandler : IRequestHandler<DeleteExpense, bool>
{
    private readonly ExpenseTrackerDbContext _db;
    private readonly ICurrentUserService _currentUserService;

    public DeleteExpenseHandler(ExpenseTrackerDbContext db, ICurrentUserService currentUserService)
    {
        _db = db;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(DeleteExpense request, CancellationToken cancellationToken)
    {
        var policy = new OwnerCanAccessOwnExpenses();
        var affectedRows = await _db.Expenses.ApplyPolicy(policy, await _currentUserService.GetCurrentUser())
            .Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);

        return affectedRows == 1;
    }
}

public record struct DeleteExpense(int Id) : IRequest<bool>;