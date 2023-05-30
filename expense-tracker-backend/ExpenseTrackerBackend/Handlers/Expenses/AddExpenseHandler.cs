using ExpenseTrackerBackend.AccessPolicies;
using ExpenseTrackerBackend.Infrastructure;
using ExpenseTrackerBackend.Services;
using FluentValidation;

namespace ExpenseTrackerBackend.Expenses;

public class AddExpenseHandler : IRequestHandler<AddExpense, Result<int>>
{
    private readonly ExpenseTrackerDbContext _db;
    private readonly IValidator<IExpenseData> _validator;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICurrentUserService _currentUser;

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

        var currentUser = await _currentUser.GetCurrentUser();

        var expenseType = _db.ExpenseTypes.ApplyPolicy(new OwnerCanAccessOwnAndStandardExpenseTypes(), currentUser)
            .FirstOrDefault(x => x.Id == request.ExpenseTypeId);

        if (expenseType == null)
        {
            return new Result<int>(new UnauthorizedAccessException("You are not allowed to add expenses of this type"));
        }

        var expense = new Expense
        {
            Description = request.Description,
            Amount = request.Amount,
            CreatedAt = _dateTimeProvider.UtcNow,
            ModifiedAt = _dateTimeProvider.UtcNow,
            OwnerId = currentUser.Id,
            ExpenseTypeId = expenseType.Id
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
    public int ExpenseTypeId { get; set; }
};