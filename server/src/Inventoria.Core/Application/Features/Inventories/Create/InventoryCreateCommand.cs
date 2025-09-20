using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventoria.Core.Application.Features.Inventories.Create;

public record InventoryCreateCommand
    (
        int UserId,
        string Name,
        DateTime CreatedAt,
        string? Description
    );
