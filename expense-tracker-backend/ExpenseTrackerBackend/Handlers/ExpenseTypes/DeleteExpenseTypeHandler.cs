using ExpenseTrackerBackend.AccessPolicies;
using ExpenseTrackerBackend.Infrastructure;
using ExpenseTrackerBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerBackend.ExpenseTypes;

public class DeleteExpenseTypeHandler : IRequestHandler<DeleteExpenseType, bool>
{
    private readonly ExpenseTrackerDbContext _db;
    private readonly ICurrentUserService _currentUserService;

    public DeleteExpenseTypeHandler(ExpenseTrackerDbContext db, ICurrentUserService currentUserService)
    {
        _db = db;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(DeleteExpenseType request, CancellationToken cancellationToken)
    {
        var expenseType = await _db.ExpenseTypes.ApplyPolicy(new OwnerCanAccessOwnAndStandardExpenseTypes(),
                await _currentUserService.GetCurrentUser())
            .Where(x => x.Id == request.Id && !x.IsArchived && x.CreatorId != null)
            .FirstOrDefaultAsync(cancellationToken);

        if (expenseType is not null)
        {
            var expensesCount = await _db.Expenses
                .ApplyPolicy(new OwnerCanAccessOwnExpenses(), await _currentUserService.GetCurrentUser())
                .Where(x => x.ExpenseTypeId == request.Id)
                .CountAsync(cancellationToken);
            
            if (expensesCount == 0)
            {
                await _db.ExpenseTypes.Where(x => x.Id == expenseType.Id)
                    .ExecuteDeleteAsync(cancellationToken: cancellationToken);
            }
            else
            {
                expenseType.IsArchived = true;
            }
            
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }

        return false;
    }
}

public record struct DeleteExpenseType(int Id) : IRequest<bool>;