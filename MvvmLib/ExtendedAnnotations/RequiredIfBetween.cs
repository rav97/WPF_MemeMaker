using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLib.ExtendedAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class RequiredIfBetween : ValidationAttribute
    {
        RequiredAttribute requiredAttribute = new RequiredAttribute();
        public string _dependentProperty1 { get; set; }
        public string _dependentProperty2 { get; set; }

        public RequiredIfBetween(string dependentProperty1, string dependentProperty2)
        {
            _dependentProperty1 = dependentProperty1;
            _dependentProperty2 = dependentProperty2;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var field1 = validationContext.ObjectType.GetProperty(_dependentProperty1);
                var field2 = validationContext.ObjectType.GetProperty(_dependentProperty2);

                if (field1 != null && field2 != null)
                {
                    object dependentValue1 = (object)field1.GetValue(validationContext.ObjectInstance, null);
                    object dependentValue2 = (object)field2.GetValue(validationContext.ObjectInstance, null);

                    if (!requiredAttribute.IsValid(value))
                    {
                        string name = validationContext.DisplayName;
                        return new ValidationResult(ErrorMessage);
                    }

                    if (value is int)
                        return CheckData((int)value, (int)dependentValue1, (int)dependentValue2);
                    else if (value is decimal)
                        return CheckData((decimal)value, (decimal)dependentValue1, (decimal)dependentValue2);
                    else if (value is DateTime)
                        return CheckData((DateTime)value, (DateTime)dependentValue1, (DateTime)dependentValue2);

                    return ValidationResult.Success;
                }
            }
            catch
            {

            }

            return new ValidationResult(ErrorMessage);
        }



        private ValidationResult CheckData(int value, int prop1, int prop2)
        {
            if (prop1 <= value && prop2 >= value)
                return ValidationResult.Success;

            return new ValidationResult(ErrorMessage);
        }

        private ValidationResult CheckData(decimal value, decimal prop1, decimal prop2)
        {
            if (prop1 <= value && prop2 >= value)
                return ValidationResult.Success;

            return new ValidationResult(ErrorMessage);
        }

        private ValidationResult CheckData(DateTime value, DateTime prop1, DateTime prop2)
        {
            if (prop1 <= value && prop2 >= value)
                return ValidationResult.Success;

            return new ValidationResult(ErrorMessage);
        }
    }
}
