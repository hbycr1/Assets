using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public static class ExtensionMethods
    {

        public static string? GetError(this ValidationResult result)
        {
            if (result.Errors?.Count != 1) return null;

            return result.Errors?.FirstOrDefault()?.ErrorMessage;
        }

        public static IEnumerable<string>? GetErrors(this ValidationResult result)
        {
            if (result.Errors?.Count > 1) return result.Errors?.Select(x => x.ErrorMessage);

            return null;
        }
    }
}
