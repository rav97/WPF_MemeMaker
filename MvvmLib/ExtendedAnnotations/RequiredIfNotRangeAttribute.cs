using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLib.ExtendedAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class RequiredIfNotRangeAttribute : ValidationAttribute
    {
        RequiredAttribute requiredAttribute = new RequiredAttribute();
        public string _dependentProperty { get; set; }
        public object[] _forbiddenValues { get; set; }

        public RequiredIfNotRangeAttribute(string dependentProperty, params object[] forbiddenValues)
        {
            this._dependentProperty = dependentProperty;
            this._forbiddenValues = forbiddenValues;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var field = validationContext.ObjectType.GetProperty(_dependentProperty);

            if (field != null)
            {
                var dependentValue = field.GetValue(validationContext.ObjectInstance, null);
                if ((dependentValue == null && _forbiddenValues == null) || !_forbiddenValues.Any(x => x.Equals(dependentValue)))
                {
                    if (!requiredAttribute.IsValid(value))
                    {
                        string name = validationContext.DisplayName;
                        return new ValidationResult(ErrorMessage);
                    }
                }
                return ValidationResult.Success;
            }
            else
                return new ValidationResult(FormatErrorMessage(_dependentProperty));
        }

    }
}
