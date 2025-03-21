using FluentValidation;
using MediatR;
using FluentResults;
using FluentValidation.Results;

namespace Vesma.Core.Behaviours;

public class ValidationBehaviour<TRequest, TResponse>(IValidator<TRequest>? validator = null)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ResultBase, new()
{
    private readonly IValidator<TRequest>? _validator = validator; 

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if(_validator is null)
        {
            return await next();
        }

        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            TResponse result = new();
            IEnumerable<Error> errors = validationResult.Errors
                .GroupBy(k => k.PropertyName)
                .Select(g => 
                {
                    var error = new Error($"'{g.Key}' validation failed");
                    error.Reasons.AddRange(g.Select(v => new Error(v.ErrorMessage)));
                    return error;
                });

            result.Reasons.AddRange(errors);

            return result;
        }

        return await next();
    }
}
