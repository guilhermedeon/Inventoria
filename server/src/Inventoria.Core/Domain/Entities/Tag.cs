namespace Inventoria.Core.Domain.Entities;

public class Tag
{
    public required int TagId { get; set; }
    public required string Name { get; set; } = null!;

    // Navigation
    public ICollection<Item> Items { get; set; } = [];
}
