namespace ExpenseTrackerBackend.Data.DTO;

public class ExpenseTypeDto : IExpenseType
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public Guid? CreatorId { get; set; }
    public bool IsStandard => CreatorId == null;
    public bool IsArchived { get; set; }
}