namespace ExpenseTrackerBackend.Data.DTO;

public class ExpenseDto : IExpenseData
{
    public int Id { get; set; }
    public string Description { get; set; } = "";
    public decimal Amount { get; set; }
    public int ExpenseTypeId { get; set; }
    public ExpenseType ExpenseType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public Guid OwnerId { get; set; }
}