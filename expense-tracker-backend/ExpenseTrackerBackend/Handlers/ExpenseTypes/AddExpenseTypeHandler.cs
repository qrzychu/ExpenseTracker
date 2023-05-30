using ExpenseTrackerBackend.Infrastructure;
using ExpenseTrackerBackend.Services;
using FluentValidation;

namespace ExpenseTrackerBackend.ExpenseTypes;

public class AddExpenseTypeHandler : IRequestHandler<AddExpenseType, Result<int>>
{
    private readonly IValidator<IExpenseType> _validator;
    private readonly ExpenseTrackerDbContext _db;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICurrentUserService _currentUserService;

    public AddExpenseTypeHandler(IValidator<IExpenseType> validator, ExpenseTrackerDbContext db, IDateTimeProvider dateTimeProvider, ICurrentUserService currentUserService)
    {
        _validator = validator;
        _db = db;
        _dateTimeProvider = dateTimeProvider;
        _currentUserService = currentUserService;
    }

    public async Task<Result<int>> Handle(AddExpenseType request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new Result<int>(new ValidationException(validationResult.Errors));
            }

            var expenseType = new ExpenseType
            {
                Name = request.Name,
                Description = request.Description,
                IsArchived = false,
                CreatedAt = _dateTimeProvider.UtcNow,
                Creator = await _currentUserService.GetCurrentUser()
            };

            await _db.ExpenseTypes.AddAsync(expenseType, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            return expenseType.Id;
        }catch(Exception e)
        {
            return new Result<int>(e);
        }
    }
}

public record struct AddExpenseType(string Name, string Description) : IRequest<Result<int>>, IExpenseType;