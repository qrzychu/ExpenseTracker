using System.Security.Claims;
using ExpenseTrackerBackend.Infrastructure;
using ExpenseTrackerBackend.Services;
using FluentValidation;

namespace ExpenseTrackerBackend.Expenses;

public class AddExpenseHandler : IRequestHandler<AddExpense, Result<int>>
{
    private readonly ExpenseTrackerDbContext _db;
    private readonly IValidator<IExpenseData> _validator;
    private readonly IDateTimeProvider _dateTimeProvider;
    private ICurrentUserService _currentUser;

    public AddExpenseHandler(ExpenseTrackerDbContext db, IValidator<IExpenseData> validator, IDateTimeProvider dateTimeProvider, ICurrentUserService currentUser)
    {
        _db = db;
        _validator = validator;
        _dateTimeProvider = dateTimeProvider;
        _currentUser = currentUser;
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
            CreatedAt = _dateTimeProvider.UtcNow,
            ModifiedAt = _dateTimeProvider.UtcNow,
            OwnerId = (await _currentUser.GetCurrentUser()).Id
        };

        await _db.Expenses.AddAsync(expense, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return expense.Id;
    }
}

public record struct AddExpense() : IRequest<Result<int>>, IExpenseData
{
    public string Description { get; set; } = "";
    public decimal Amount { get; set; } = 0;
};