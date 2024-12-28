using Core.Extensions;
using FluentValidation.Results;
using HotChocolate;

namespace Core.Validation;

public class BusinessValidationErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        var errors = new List<IError>();
        
        if (error.Exception is FluentValidation.ValidationException validationException)
        {
            errors.AddRange(validationException
                .Errors.Select(validationFailure => CreateErrorBuilder(validationFailure).Build()));
            
            return new AggregateError(errors);
        }
        
        return error;
    }
    
    protected virtual IErrorBuilder CreateErrorBuilder(
        ValidationFailure failure)
    {
        var errorBuilder = ErrorBuilder.New()
            .SetMessage(failure.ErrorMessage)
            .SetCode("Business_ValidationError")
            .SetExtension("errorMessage", (object) failure.ErrorMessage)
            .SetExtension("attemptedValue", failure.AttemptedValue)
            .SetExtension("severity", (object) failure.Severity)
            .SetTraceIdentifiers();
        
        if (!string.IsNullOrWhiteSpace(failure.PropertyName))
            errorBuilder = errorBuilder.SetExtension("propertyName", (object) failure.PropertyName);
        return errorBuilder;
    }
}