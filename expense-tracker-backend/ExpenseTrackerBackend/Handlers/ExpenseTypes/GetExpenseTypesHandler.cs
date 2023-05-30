using ExpenseTrackerBackend.AccessPolicies;
using ExpenseTrackerBackend.Infrastructure;
using ExpenseTrackerBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerBackend.ExpenseTypes;

public class GetExpenseTypesHandler : IRequestHandler<GetExpenseTypes, IEnumerable<ExpenseType>>
{
    private readonly ExpenseTrackerDbContext _db;
    private readonly ICurrentUserService _currentUser;

    public GetExpenseTypesHandler(ExpenseTrackerDbContext db, ICurrentUserService currentUser)
    {
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<IEnumerable<ExpenseType>> Handle(GetExpenseTypes request, CancellationToken cancellationToken)
    {
        var policy = new OwnerCanAccessOwnAndStandardExpenseTypes();
        
        return await _db.ExpenseTypes.AsNoTracking()
            .ApplyPolicy(policy, await _currentUser.GetCurrentUser())
            .Where(x => !x.IsArchived)
            .ToListAsync(cancellationToken);
    }
}

public record struct GetExpenseTypes : IRequest<IEnumerable<ExpenseType>>;