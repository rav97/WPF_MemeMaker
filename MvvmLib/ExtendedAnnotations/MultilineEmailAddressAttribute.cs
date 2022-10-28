using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLib.ExtendedAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class MultilineEmailAddressAttribute : DataTypeAttribute
    {
        private readonly EmailAddressAttribute emailAddressAttribute = new EmailAddressAttribute();

        public MultilineEmailAddressAttribute() : base(DataType.EmailAddress) { }

        public override bool IsValid(object value)
        {
            var emailAddr = Convert.ToString(value);
            if (string.IsNullOrWhiteSpace(emailAddr)) return false;

            var emails = emailAddr.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return emails.All(e => emailAddressAttribute.IsValid(e));
        }
    }
}
