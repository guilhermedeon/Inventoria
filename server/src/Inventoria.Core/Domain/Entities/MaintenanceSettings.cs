namespace Inventoria.Core.Domain.Entities;

public class MaintenanceSettings
{
    public required int ItemId { get; set; }
    public int? IntervalMonths { get; set; }
    public DateTime? NextDueDate { get; set; }

    // Navigation
    public Item? Item { get; set; }
}