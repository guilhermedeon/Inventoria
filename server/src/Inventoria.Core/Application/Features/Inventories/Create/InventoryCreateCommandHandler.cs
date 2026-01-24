using Inventoria.Core.Domain.Abstractions;
using Inventoria.Core.Domain.Entities.Inventories;
using Inventoria.Core.Domain.Entities.Inventories.Abstractions;
using Inventoria.SharedKernel;

namespace Inventoria.Core.Application.Features.Inventories.Create;

public class InventoryCreateCommandHandler(IInventoryRepository inventoryRepository)
    : IHandler<InventoryCreateCommand, Result>
{
    public async Task<Result> HandleAsync(InventoryCreateCommand command)
    {
        var inventory = new Inventory
        {
            UserId = command.UserId,
            Name = command.Name,
            CreatedAt = command.CreatedAt,
            Description = command.Description
        };

        return await inventoryRepository.AddAsync(inventory);
    }
}