using MediatR;
using FluentResults;

namespace Vesma.Core.Base;

public interface IRequestWrapper<TResponse> : IRequest<Result<TResponse>>
{ 
}