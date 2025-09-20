namespace Inventoria.Core.Domain.Database;

public class MigrationHistoric
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required DateTime AppliedOn { get; set; }
}