using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventoria.Core.Domain.Abstractions;
public interface IHandler<T>
{
    Task HandleAsync(T request);
}

public interface IHandler<TRequest, TResponse>
{
    Task<TResponse> HandleAsync(TRequest request);
}
