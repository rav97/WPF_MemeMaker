using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLib.ExtendedAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class DateValidAttribute : ValidationAttribute
    {
        public string[] _formats { get; set; }

        public DateValidAttribute()
        {
            _formats = null;
        }

        public DateValidAttribute(params string[] format)
        {
            _formats = format;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                string input = value.ToString();
                DateTime expectedDate;

                if(_formats != null && _formats.Length > 0 )
                {
                    if(!DateTime.TryParseExact(input, _formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out expectedDate))
                        return new ValidationResult("Nieprawidłowy format daty");
                }
                else
                {
                    if(!DateTime.TryParse(input, out expectedDate))
                        return new ValidationResult("Nieprawidłowy format daty");
                }

                return ValidationResult.Success;
            }
            catch
            {
                return new ValidationResult("Walidacja zgłosiła wyjątek");
            }
        }
    }
}
