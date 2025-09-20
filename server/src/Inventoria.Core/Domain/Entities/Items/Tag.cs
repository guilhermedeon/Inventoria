namespace Inventoria.Core.Domain.Entities.Items;

public class Tag
{
    public int TagId { get; set; }
    public required string Name { get; set; } = null!;

    // Navigation
    public ICollection<Item> Items { get; set; } = [];
}
