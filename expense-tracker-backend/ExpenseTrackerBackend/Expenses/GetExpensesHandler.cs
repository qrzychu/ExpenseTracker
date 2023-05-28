namespace ExpenseTrackerBackend.Expenses;

public class GetExpensesHandler : IRequestHandler<GetExpenses, IAsyncEnumerable<Expense>>
{
    private readonly ExpenseTrackerDbContext _db;

    public GetExpensesHandler(ExpenseTrackerDbContext db)
    {
        _db = db;
    }

    public Task<IAsyncEnumerable<Expense>> Handle(GetExpenses request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_db.Expenses.AsAsyncEnumerable());
    }
}

public record struct GetExpenses : IRequest<IAsyncEnumerable<Expense>>;
