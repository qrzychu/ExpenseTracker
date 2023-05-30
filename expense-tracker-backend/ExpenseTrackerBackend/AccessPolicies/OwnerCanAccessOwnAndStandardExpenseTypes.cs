namespace ExpenseTrackerBackend.AccessPolicies;

public class OwnerCanAccessOwnAndStandardExpenseTypes : IAccessPolicy<ExpenseType>
{
    public IQueryable<ExpenseType> Apply(IQueryable<ExpenseType> queryable, User user)
    {
        return queryable.Where(x => x.CreatorId == user.Id || x.CreatorId == null);
    }

    public bool CanAccess(ExpenseType entity, User user)
    {
        return entity.CreatorId == user.Id || entity.CreatorId == null;
    }
}