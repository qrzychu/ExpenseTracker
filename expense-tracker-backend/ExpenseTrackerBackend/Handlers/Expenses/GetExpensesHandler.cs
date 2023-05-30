using ExpenseTrackerBackend.AccessPolicies;
using ExpenseTrackerBackend.Data.DTO;
using ExpenseTrackerBackend.Infrastructure;
using ExpenseTrackerBackend.Services;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerBackend.Expenses;

public class GetExpensesHandler : IRequestHandler<GetExpenses, IEnumerable<ExpenseDto>>
{
    private readonly ExpenseTrackerDbContext _db;
    private readonly ICurrentUserService _currentUser;

    public GetExpensesHandler(ExpenseTrackerDbContext db, ICurrentUserService currentUser)
    {
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<IEnumerable<ExpenseDto>> Handle(GetExpenses request, CancellationToken cancellationToken)
    {
        return _db.Expenses.AsNoTracking()
            .ApplyPolicy(new OwnerCanAccessOwnExpenses(), await _currentUser.GetCurrentUser())
            .Select(x => x.MapToDto());
    }
}

public record struct GetExpenses : IRequest<IEnumerable<ExpenseDto>>;