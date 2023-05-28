using ExpenseTrackerBackend.Infrastructure;
using FluentValidation;

namespace ExpenseTrackerBackend.Data.Models;

public class Expense : IExpense
{
    public int Id { get; set; }
    public string Description { get; set; } = "";
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}

public interface IExpense
{
    string Description { get; set; }
    decimal Amount { get; set; }
}

public class ExpenseValidator : AbstractValidator<IExpense>
{
    public ExpenseValidator(IDateTimeProvider dateTimeProvider)
    {
        RuleFor(x => x.Description).MaximumLength(255);
        RuleFor(x => x.Amount).GreaterThan(0);
    }
}