using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventoria.Infra.Data;

public class MigrationHistoric
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required DateTime AppliedOn { get; set; }
}