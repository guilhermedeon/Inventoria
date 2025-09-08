namespace Inventoria.Core.Domain.Entities.Transactions;

public class InventoryTransaction
{
    public required int TransactionId { get; set; }

    public required DateTime Date { get; set; }
    public required TransactionType Type { get; set; }
    public string? Notes { get; set; }

    // Navigation
    public ICollection<InventoryTransactionLine> Lines { get; set; } = [];
}
