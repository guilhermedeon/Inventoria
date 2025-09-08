using Inventoria.Core.Domain.Entities.Items;

namespace Inventoria.Core.Domain.Entities.Transactions;

public class InventoryTransactionLine
{
    public required int LineId { get; set; }

    public required int TransactionId { get; set; }
    public required int ItemId { get; set; }
    public required int QuantityChange { get; set; }

    public int? SourceInventoryId { get; set; }
    public int? TargetInventoryId { get; set; }

    // Navigation
    public InventoryTransaction? Transaction { get; set; }
    public Item? Item { get; set; }
    public Inventory? SourceInventory { get; set; }
    public Inventory? TargetInventory { get; set; }
}
