using MediatR;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Vesma.Core.Behaviours;

public class ExceptionBehaviour<TRequest, TResponse>(ILogger<ExceptionBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ResultBase, new()
{
    private readonly ILogger<ExceptionBehaviour<TRequest, TResponse>> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogError($"Unhandled exception occurred during processing of {nameof(TRequest)}");
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unhandled exception occurred during processing of {nameof(TRequest)}");
            TResponse result = new();
            result.Reasons.Add(new Error("Unable to process your request, please try again latter."));
            return result;

        }
    }
}
