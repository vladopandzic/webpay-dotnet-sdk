using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPay
{
    public static class MyValidatorExtensions
    {

        public static IRuleBuilderOptions<T, TProperty> WithErrorCode<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, int code)
        {
            return rule.WithState(x => code);
        }
        public static List<ValidationError> ToValidationErrors(this IList<ValidationFailure> validationFailures)
        {
            return validationFailures.Select(x => new ValidationError
            {
                ErrorCode = x.ErrorCode,
                ErrorMessage = x.ErrorMessage,
                CustomState = x.CustomState,
                PropertyName = x.PropertyName,
                AttemptedValue = x.AttemptedValue
            }).ToList();
        }
    }
}
