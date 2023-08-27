using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Extensions;

public static class ValidationExtensions
{
    public static ModelStateDictionary ToModelStateDictionary(this ValidationResult result)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var failure in result.Errors)
        {
            modelStateDictionary.AddModelError(failure.PropertyName, failure.ErrorMessage);
        }

        return modelStateDictionary;
    }
}