using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Vesma.Core.Behaviours;

public class LoggingBehaviour<TRequest, TResponse>(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResultBase
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Request Handling: { name } {@request }",
            typeof(TRequest).Name, JsonSerializer.Serialize(request));

        var response = await next();

        _logger.LogInformation(
            "Response Handling: { name } {@response }",
            typeof(TResponse).Name,
            response.IsFailed ? JsonSerializer.Serialize(response.Errors) : JsonSerializer.Serialize(response));

        return response;
    }
}