namespace Inventoria.Core.Application.Features.Inventories.Create;

public record InventoryCreateCommand(
    int UserId,
    string Name,
    DateTime CreatedAt,
    string? Description
);