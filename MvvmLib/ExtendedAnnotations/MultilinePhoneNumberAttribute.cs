using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLib.ExtendedAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class MultilinePhoneNumberAttribute : DataTypeAttribute
    {
        private readonly DataTypeAttribute phoneNumberAttribute = new DataTypeAttribute(DataType.PhoneNumber);
        private RegularExpressionAttribute regex = new RegularExpressionAttribute(ValidModel.TELEFON_REGEX) { ErrorMessage = "Co najmniej jeden z numerów jest błędny" };

        public MultilinePhoneNumberAttribute() : base(DataType.PhoneNumber) { }

        public override bool IsValid(object value)
        {
            var phoneNums = Convert.ToString(value);
            if (string.IsNullOrWhiteSpace(phoneNums)) return false;

            var numers = phoneNums.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return numers.All(e => phoneNumberAttribute.IsValid(e) && regex.IsValid(e));
        }
    }
}
