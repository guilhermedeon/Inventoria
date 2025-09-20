using Inventoria.Core.Domain.Entities.Inventories;
using Inventoria.Core.Domain.Entities.Inventories.Abstractions;

namespace Inventoria.Core.Application.Features.Inventories.Create;

public class InventoryCreateCommandHandler(IInventoryRepository inventoryRepository)
{
    public async Task HandleAsync(InventoryCreateCommand command)
    {
        var inventory = new Inventory
        {
            UserId = command.UserId,
            Name = command.Name,
            CreatedAt = command.CreatedAt,
            Description = command.Description
        };

        await inventoryRepository.AddAsync(inventory);
    }
}

