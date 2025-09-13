using Inventoria.Core.Domain.Entities.Inventories;

namespace Inventoria.Core.Domain.Entities.Users;

public class User
{
    public required int UserId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }

    // Navigation
    public ICollection<Inventory> Inventories { get; set; } = [];
}
