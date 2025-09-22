using Inventoria.Core.Domain.Entities.Inventories;
using Inventoria.Core.Domain.Entities.Maintenances;

namespace Inventoria.Core.Domain.Entities.Items;

public class Item
{
    public int ItemId { get; set; }
    public required int InventoryId { get; set; }
    public required string Name { get; set; }
    public required int Quantity { get; set; }
    public required bool IsConsumable { get; set; }
    public required DateTime CreatedAt { get; set; }
    public string? Description { get; set; }
    public int? ResupplyThreshold { get; set; }
    public MaintenanceSettings? MaintenanceSettings { get; set; }


    // Navigation
    public Inventory? Inventory { get; set; }
    public ICollection<Tag> Tags { get; set; } = [];
    public ICollection<MaintenanceRecord> MaintenanceRecords { get; set; } = [];
}