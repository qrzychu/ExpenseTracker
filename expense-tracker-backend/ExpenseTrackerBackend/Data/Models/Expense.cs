namespace ExpenseTrackerBackend.Data.Models;

public class Expense
{
    public int Id { get; set; }
    public string Description { get; set; } = "";
    public decimal Amount { get; set; }
    public DateTime AddedAt { get; set; }
}