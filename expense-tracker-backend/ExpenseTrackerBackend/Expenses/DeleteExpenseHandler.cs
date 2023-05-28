using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerBackend.Expenses;

public class DeleteExpenseHandler : IRequestHandler<DeleteExpense, bool>
{
    private readonly ExpenseTrackerDbContext _db;

    public DeleteExpenseHandler(ExpenseTrackerDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(DeleteExpense request, CancellationToken cancellationToken)
    {
        var affectedRows = await _db.Expenses.Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
        return affectedRows == 1;
    }
}

public record struct DeleteExpense(int Id) : IRequest<bool>;