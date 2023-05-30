using ExpenseTrackerBackend.Infrastructure;
using FluentValidation;

namespace ExpenseTrackerBackend.Data.Models;

public class Expense : IExpenseData
{
    public int Id { get; set; }
    public string Description { get; set; } = "";
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public Guid OwnerId { get; set; }
    public User Owner { get; set; } = null!;

    public ExpenseType ExpenseType { get; set; } = null!;
    public int ExpenseTypeId { get; set; }
}

public interface IExpenseData
{
    string Description { get; set; }
    decimal Amount { get; set; }
    int ExpenseTypeId { get; set; }
}

public interface IExpenseWithRelations : IExpenseData
{
    ExpenseType ExpenseType { get; set; }
}

public class ExpenseValidator : AbstractValidator<IExpenseData>
{
    public ExpenseValidator(IDateTimeProvider dateTimeProvider)
    {
        RuleFor(x => x.Description).MaximumLength(255);
        RuleFor(x => x.Amount).GreaterThan(0);
    }
}