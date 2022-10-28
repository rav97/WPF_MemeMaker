using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLib.ExtendedAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class RequiredIfIdNotRangeAttribute : ValidationAttribute
    {
        RequiredAttribute requiredAttribute = new RequiredAttribute();
        public string _dependentProperty { get; set; }
        public int[] _forbiddenIds { get; set; }

        public RequiredIfIdNotRangeAttribute(string dependentProperty, params int[] forbiddenIds)
        {
            this._dependentProperty = dependentProperty;
            this._forbiddenIds = forbiddenIds;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var field = validationContext.ObjectType.GetProperty(_dependentProperty);

            if (field != null)
            {
                var dependentValue = field.GetValue(validationContext.ObjectInstance, null);
                bool notContain = dependentValue == null ? false : !_forbiddenIds.Any(x => (int)dependentValue.GetType().GetProperty("id").GetValue(dependentValue) == x);
                if ((dependentValue == null && _forbiddenIds == null) || notContain)
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
