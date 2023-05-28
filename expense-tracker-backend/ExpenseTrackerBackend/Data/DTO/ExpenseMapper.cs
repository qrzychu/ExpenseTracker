using Riok.Mapperly.Abstractions;

namespace ExpenseTrackerBackend.Data.DTO;

[Mapper]
public static partial class ExpenseMapper
{
    public static partial ExpenseDto MapToDto(this Expense entity);
}