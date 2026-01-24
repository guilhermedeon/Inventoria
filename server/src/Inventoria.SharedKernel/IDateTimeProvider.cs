namespace Inventoria.SharedKernel;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
