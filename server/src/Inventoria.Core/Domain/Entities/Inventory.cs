using Inventoria.Core.Domain.Entities.Items;

namespace Inventoria.Core.Domain.Entities;

public class Inventory
{
    public required int InventoryId { get; set; }
    public required int UserId { get; set; }
    public required string Name { get; set; }
    public required DateTime CreatedAt { get; set; }
    public string? Description { get; set; }

    // Navigation
    public User? User { get; set; }
    public ICollection<Item> Items { get; set; } = [];
}
