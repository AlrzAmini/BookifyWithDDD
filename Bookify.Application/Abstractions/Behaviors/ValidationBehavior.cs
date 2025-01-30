using Bookify.Application.Abstractions.Messaging;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Bookify.Application.Abstractions.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    protected IEnumerable<IValidator<TRequest>> Validators { get; } = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!Validators.Any())
        {
            return await next();
        }

        var context =
            new ValidationContext<TRequest>(request);

        var validationResults =
            await Task.WhenAll
                (Validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures =
            validationResults
                .SelectMany(current => current.Errors)
                .Where(current => current != null)
                .ToList();

        if (failures.Count > 0)
        {
            throw new ValidationException(errors: failures);
        }

        return await next();
    }
}