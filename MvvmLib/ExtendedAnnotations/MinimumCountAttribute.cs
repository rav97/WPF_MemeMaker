using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLib.ExtendedAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class MinimumCountAttribute : ValidationAttribute
    {
        public int minimumCount { get; set; }

        public MinimumCountAttribute(int minCount)
        {
            minimumCount = minCount;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is ICollection)
            {
                var collection = value as ICollection;

                if (collection.Count < minimumCount)
                    return new ValidationResult(ErrorMessage);
                else
                    return ValidationResult.Success;
            }
            else
                return new ValidationResult("Właściwość nie jest kolekcją");
        }
    }
}
