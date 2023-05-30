using LanguageExt;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerBackend.Data;

public interface IExpenseTrackerDbContext
{
    DbSet<Expense> Expenses { get; set; }
}

public class ExpenseTrackerDbContext: IdentityDbContext<User, Role, Guid>, IExpenseTrackerDbContext
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseType> ExpenseTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ExpenseTrackerDbContext).Assembly);
    }

    public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options) : base(options)
    {
    }
}