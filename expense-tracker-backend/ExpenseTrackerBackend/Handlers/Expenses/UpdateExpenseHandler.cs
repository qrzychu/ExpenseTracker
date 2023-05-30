using ExpenseTrackerBackend.AccessPolicies;
using ExpenseTrackerBackend.Infrastructure;
using ExpenseTrackerBackend.Services;
using FluentValidation;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerBackend.Expenses;

public class UpdateExpenseHandler : IRequestHandler<UpdateExpense, Result<int>>
{
    private readonly ExpenseTrackerDbContext _db;
    private readonly IValidator<IExpenseData> _validator;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICurrentUserService _currentUserService;

    public UpdateExpenseHandler(ExpenseTrackerDbContext db, IValidator<IExpenseData> validator,
        IDateTimeProvider dateTimeProvider, ICurrentUserService currentUserService)
    {
        _db = db;
        _validator = validator;
        _dateTimeProvider = dateTimeProvider;
        _currentUserService = currentUserService;
    }

    public async Task<Result<int>> Handle(UpdateExpense request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new Result<int>(new ValidationException(validationResult.Errors));
        }

        var policy = new OwnerCanAccessOwnExpenses();

        var expense =
            await _db.Expenses.ApplyPolicy(policy, await _currentUserService.GetCurrentUser())
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken) ?? new Expense
            {
                CreatedAt = _dateTimeProvider.UtcNow
            };

        expense.Description = request.Description;
        expense.Amount = request.Amount;
        expense.ModifiedAt = _dateTimeProvider.UtcNow;

        _db.Attach(expense).State = expense.Id == 0 ? EntityState.Added : EntityState.Modified;

        await _db.SaveChangesAsync(cancellationToken);

        return expense.Id;
    }
}

public record struct UpdateExpense
    (int Id, string Description, decimal Amount, int ExpenseTypeId) : IRequest<Result<int>>, IExpenseData;