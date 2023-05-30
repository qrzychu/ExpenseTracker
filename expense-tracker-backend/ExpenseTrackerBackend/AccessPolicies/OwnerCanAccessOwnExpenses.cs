namespace ExpenseTrackerBackend.AccessPolicies;

public class OwnerCanAccessOwnExpenses : IAccessPolicy<Expense>
{
    public IQueryable<Expense> Apply(IQueryable<Expense> queryable, User user)
    {
        return queryable.Where(x => x.OwnerId == user.Id);
    }

    public bool CanAccess(Expense entity, User user)
    {
        return entity.OwnerId == user.Id;
    }
}

public interface IAccessPolicy<T>
{
    IQueryable<T> Apply(IQueryable<T> queryable, User user);
    bool CanAccess(T entity, User user);
}