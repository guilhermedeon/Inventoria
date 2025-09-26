namespace Inventoria.Core.Application.Features.Inventories.Update;

public record InventoryUpdateCommand
(
    int InventoryId,
    int UserId,
    string Name,
    DateTime CreatedAt,
    string? Description
);

