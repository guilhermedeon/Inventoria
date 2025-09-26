using Inventoria.Core.Domain.Abstractions;
using Inventoria.Core.Domain.Entities.Inventories.Abstractions;

namespace Inventoria.Core.Application.Features.Inventories.Delete;

public class InventoryDeleteByIdCommandHandler(IInventoryRepository inventoryRepository)
    : IHandler<InventoryDeleteByIdCommand>
{
    public async Task HandleAsync(InventoryDeleteByIdCommand request)
    {
        await inventoryRepository.DeleteByIdAsync(request.InventoryId);
    }
}
