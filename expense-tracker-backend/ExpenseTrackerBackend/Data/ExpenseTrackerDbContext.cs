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
    
    public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options) : base(options)
    {
    }
}