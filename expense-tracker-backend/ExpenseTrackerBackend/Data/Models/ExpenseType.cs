using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTrackerBackend.Data.Models;

public class ExpenseType : IExpenseType
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime CreatedAt { get; set; }

    public Guid? CreatorId { get; set; }
    public User? Creator { get; set; }
    public bool IsArchived { get; set; }

    public string NormalizedName
    {
        get => Name.ToLowerInvariant().Trim();
        private set { }
    }

    public enum StandardExpenseTypes
    {
        Food = 1,
        Entertainment = 2,
        Travel = 3,
    }
}

public interface IExpenseType
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class ExpenseTypeValidator : AbstractValidator<IExpenseType>
{
    public ExpenseTypeValidator()
    {
        RuleFor(x => x.Name).NotNull().MinimumLength(2).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Description).NotNull().MaximumLength(255);
    }
}

internal class ExpenseTypeConfiguration : IEntityTypeConfiguration<ExpenseType>
{
    public void Configure(EntityTypeBuilder<ExpenseType> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(255);
        builder.Property(x => x.Description).HasMaxLength(255);

        builder.HasIndex(x => new { x.CreatorId, x.NormalizedName }).IsUnique();

        builder.HasData(new List<ExpenseType>
        {
            new ExpenseType
            {
                Id = (int)ExpenseType.StandardExpenseTypes.Food,
                Name = "Food",
                Description = "Food groceries",
                CreatedAt = DateTime.UtcNow
            },
            new ExpenseType
            {
                Id = (int)ExpenseType.StandardExpenseTypes.Entertainment,
                Name = "Entertainment",
                Description = "Entertainment expenses",
                CreatedAt = DateTime.UtcNow
            },
            new ExpenseType
            {
                Id = (int)ExpenseType.StandardExpenseTypes.Travel,
                Name = "Travel",
                Description = "Travel expenses",
                CreatedAt = DateTime.UtcNow
            }
        });
    }
}