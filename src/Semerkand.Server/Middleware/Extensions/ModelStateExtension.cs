using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Semerkand.Server.Middleware.Wrappers;

namespace Semerkand.Server.Middleware.Extensions
{
    public static class ModelStateExtension
    {
        public static IEnumerable<ValidationError> AllErrors(this ModelStateDictionary modelState)
        {
            return modelState.Keys.SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage))).ToList();
        }
    }
}
