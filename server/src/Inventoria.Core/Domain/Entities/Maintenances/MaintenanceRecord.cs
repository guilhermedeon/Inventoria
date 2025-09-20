using Inventoria.Core.Domain.Entities.Items;

namespace Inventoria.Core.Domain.Entities.Maintenances;

public class MaintenanceRecord
{
    public int MaintenanceId { get; set; }
    public required int ItemId { get; set; }
    public required DateTime DatePerformed { get; set; }
    public decimal? Cost { get; set; }
    public string? Notes { get; set; }

    // Navigation
    public Item? Item { get; set; }
}
