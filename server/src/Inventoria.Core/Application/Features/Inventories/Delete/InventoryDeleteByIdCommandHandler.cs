using Inventoria.Core.Domain.Abstractions;
using Inventoria.Core.Domain.Entities.Inventories.Abstractions;
using Inventoria.SharedKernel;

namespace Inventoria.Core.Application.Features.Inventories.Delete;

public class InventoryDeleteByIdCommandHandler(IInventoryRepository inventoryRepository)
    : IHandler<InventoryDeleteByIdCommand, Result>
{
    public async Task<Result> HandleAsync(InventoryDeleteByIdCommand request)
    {
        return await inventoryRepository.DeleteByIdAsync(request.InventoryId);
    }
}
