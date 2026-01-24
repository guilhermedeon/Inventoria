using Inventoria.Core.Domain.Abstractions;
using Inventoria.Core.Domain.Entities.Inventories;
using Inventoria.Core.Domain.Entities.Inventories.Abstractions;
using Inventoria.SharedKernel;

namespace Inventoria.Core.Application.Features.Inventories.Update;

public class InventoryUpdateCommandHandler(IInventoryRepository inventoryRepository)
    : IHandler<InventoryUpdateCommand, Result>
{
    public async Task<Result> HandleAsync(InventoryUpdateCommand request)
    {
        var inventory = new Inventory
        {
            InventoryId = request.InventoryId,
            UserId = request.UserId,
            Name = request.Name,
            CreatedAt = request.CreatedAt,
            Description = request.Description
        };

        return await inventoryRepository.UpdateAsync(inventory);
    }
}