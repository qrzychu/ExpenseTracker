using ExpenseTrackerBackend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerBackend.Data;

public interface IExpenseTrackerDbContext
{
    DbSet<Expense> Expenses { get; set; }
}

public class ExpenseTrackerDbContext: DbContext, IExpenseTrackerDbContext
{
    public DbSet<Expense> Expenses { get; set; }
    
    public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options) : base(options)
    {
    }
}