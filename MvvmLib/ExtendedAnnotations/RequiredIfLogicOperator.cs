using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLib.ExtendedAnnotations
{
    public enum AdnotationOperatorType
    {
        Less,
        More,
        LessEqual,
        MoreEqual
    }


    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class RequiredIfLogicOperator : ValidationAttribute
    {
        RequiredAttribute requiredAttribute = new RequiredAttribute();
        public string _dependentProperty { get; set; }
        public AdnotationOperatorType _type;

        public RequiredIfLogicOperator(string dependentProperty, AdnotationOperatorType type)
        {
            _dependentProperty = dependentProperty;
            _type = type;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           var field = validationContext.ObjectType.GetProperty(_dependentProperty);

            if (field != null)
            {
                decimal dependentValue = (decimal)field.GetValue(validationContext.ObjectInstance, null);
                if (!requiredAttribute.IsValid(value))
                {
                    string name = validationContext.DisplayName;
                    return new ValidationResult(ErrorMessage);
                }

                switch (_type)
                {
                    case AdnotationOperatorType.Less:
                        if (dependentValue > (decimal)value)
                            return ValidationResult.Success;
                        break;
                    case AdnotationOperatorType.More:
                        if (dependentValue < (decimal)value)
                            return ValidationResult.Success;
                        break;
                    case AdnotationOperatorType.LessEqual:
                        if (dependentValue >= (decimal)value)
                            return ValidationResult.Success;
                        break;
                    case AdnotationOperatorType.MoreEqual:
                        if (dependentValue <= (decimal)value)
                            return ValidationResult.Success;
                        break;
                    default:
                        break;
                } 
            }
           
            return new ValidationResult(FormatErrorMessage(_dependentProperty));
        }

    }
}
