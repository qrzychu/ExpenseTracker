namespace ExpenseTrackerBackend.Expenses;

public class GetExpenseHandler : IRequestHandler<GetExpense, Expense?>
{
    private readonly ExpenseTrackerDbContext _db;

    public GetExpenseHandler(ExpenseTrackerDbContext db)
    {
        _db = db;
    }

    public async Task<Expense?> Handle(GetExpense request, CancellationToken cancellationToken)
    {
        return await _db.Expenses.FindAsync(new object[] {request.Id}, cancellationToken);
    }
}

public record struct GetExpense(int Id) : IRequest<Expense?>;