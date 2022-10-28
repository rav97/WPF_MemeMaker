using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLib.ExtendedAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class MultilineStringLengthAttribute : StringLengthAttribute
    {
        private StringLengthAttribute stringLengthAttribute;

        public MultilineStringLengthAttribute(int MinLength, int MaxLength) : base(11000)
        {
            stringLengthAttribute = new StringLengthAttribute(MaxLength) { MinimumLength = MinLength };
        }

        public override bool IsValid(object value)
        {
            var phoneNums = Convert.ToString(value);
            if (string.IsNullOrWhiteSpace(phoneNums)) return false;

            var numers = phoneNums.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return numers.All(e => stringLengthAttribute.IsValid(e));
        }
    }
}
