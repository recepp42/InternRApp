using FluentValidation;
using MediatR;
using ValidationException = backend.Application.Common.Exceptions.ValidationException;

namespace backend.Application.Common.Behaviours;
public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            try
            {
                var validationResults = await Task.WhenAll(
         _validators.Select(v =>
            v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
          .Where(r => r.Errors.Any())
          .SelectMany(r => r.Errors)
          .ToList();
                if (failures.Any())
                    throw new ValidationException(failures);
            }
            catch (Exception ex)
            {
                var result = ex;
                throw;
            }


        }
        return await next();
    }
}
