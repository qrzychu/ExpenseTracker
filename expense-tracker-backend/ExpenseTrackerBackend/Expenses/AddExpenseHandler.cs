using ExpenseTrackerBackend.Infrastructure;
using FluentValidation;

namespace ExpenseTrackerBackend.Expenses;

public class AddExpenseHandler : IRequestHandler<AddExpense, Result<int>>
{
    private readonly ExpenseTrackerDbContext _db;
    private readonly IValidator<IExpense> _validator;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AddExpenseHandler(ExpenseTrackerDbContext db, IValidator<IExpense> validator, IDateTimeProvider dateTimeProvider)
    {
        _db = db;
        _validator = validator;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<int>> Handle(AddExpense request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new Result<int>(new ValidationException(validationResult.Errors));
        }

        var expense = new Expense
        {
            Description = request.Description,
            Amount = request.Amount,
            CreatedAt = _dateTimeProvider.UtcNow
        };

        await _db.Expenses.AddAsync(expense, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return expense.Id;
    }
}

public record struct AddExpense() : IRequest<Result<int>>, IExpense
{
    public string Description { get; set; } = "";
    public decimal Amount { get; set; } = 0;
};