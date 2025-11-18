using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = FluentValidation.ValidationException;

namespace UPCH.Bookstore.Application.Common.Behaviors
{
    // Pipeline behavior que ejecuta validadores de FluentValidation para requests que tengan validadores registrados.
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Any())
            {
                // Si la respuesta es un Result<T>, devolvemos un Result<T>.Failure vía reflexión.
                var messages = failures.Select(f => f.ErrorMessage).ToList();

                var responseType = typeof(TResponse);
                if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(UPCH.Bookstore.Application.Common.Models.Result<>))
                {
                    // Construir Result<T>.Failure usando reflexión
                    var failureMethod = responseType.GetMethod("Failure", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static, null, new[] { typeof(IEnumerable<string>) }, null);
                    if (failureMethod != null)
                    {
                        var result = failureMethod.Invoke(null, new object[] { messages });
                        return (TResponse)result!;
                    }
                }

                // Si no es Result<T>, lanzar ValidationException de FluentValidation
                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
