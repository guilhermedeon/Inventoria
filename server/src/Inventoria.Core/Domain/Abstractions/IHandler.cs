namespace Inventoria.Core.Domain.Abstractions;

public interface IHandler<T>
{
    Task HandleAsync(T request);
}

public interface IHandler<TRequest, TResponse>
{
    Task<TResponse> HandleAsync(TRequest request);
}