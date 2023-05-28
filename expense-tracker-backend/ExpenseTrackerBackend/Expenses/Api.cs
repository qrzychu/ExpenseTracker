using ExpenseTrackerBackend.Data;

namespace ExpenseTrackerBackend.Expenses;

public static class Api
{
    public static void MapExpensesApi(this WebApplication app)
    {
        app.MapGet("/expenses", (ExpenseTrackerDbContext dbContext) => dbContext.Expenses.AsAsyncEnumerable());
    }
}