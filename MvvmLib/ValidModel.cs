using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLib
{
    public class ValidModel : INotifyDataErrorInfo
    {

        #region [ REGEX ]

        public const string PASSWORD_REGEX = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-_]).{8,}$";
        public const string NIP_REGEX = @"^[0-9]{10}$|^(\d{3}-\d{3}-\d{2}-\d{2})$|^(\d{3}-\d{2}-\d{2}-\d{3})$";
        public const string KRS_REGEX = @"^[0-9]{10}$|^(\d{3}-\d{3}-\d{2}-\d{2})$|^(\d{3}-\d{2}-\d{2}-\d{3})$";
        public const string REGON_REGEX = @"^\d{9}$|^(\d{3} \d{3} \d{3})$|^(\d{3}-\d{3}-\d{3})$";
        public const string KOD_POCZTOWY_REGEX = @"\d{2}-\d{3}$";
        public const string IMIE_REGEX = @"^[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ]{3,30}$";
        public const string NAZWISKO_REGEX = @"^[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ][A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ\-\']{2,99}$";
        public const string KWOTA_DODATNIA = @"^\s*(?=.*[1-9])\d*(?:[.,]\d{1,2})?\s*$";




        public const string TELEFON_REGEX = @"^((?:\s*\d){9,11})|(\+(?:\s*\d){9,11})$";
        public const string LICZBA_REGEX = @"^[1-9]+[0-9]*$";
        public const string MONEY_REGEX = @"(^[0-9]+([.,][0-9]{1,2})?)|(^[-]{1}[0-9]+([.,][0-9]{1,2})?)$";
        public const string PESEL_REGEX = @"^[0-9]{11}$|^(\d{2} \d{2} \d{2} \d{2} \d{3})$";
        public const string NRB_REGEX = @"^\d{26}$|^(\d{2} \d{4} \d{4} \d{4} \d{4} \d{4} \d{4})$";

        #endregion

        private readonly Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();

        public bool HasErrors => _errorsByPropertyName.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (propertyName == null) return null;
            return _errorsByPropertyName.ContainsKey(propertyName) ?
                _errorsByPropertyName[propertyName] : null;
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected void AddError(string propertyName, string error)
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName))
                _errorsByPropertyName[propertyName] = new List<string>();

            if (!_errorsByPropertyName[propertyName].Contains(error))
            {
                _errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        private void ClearErrors(string propertyName)
        {
            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        private object GetValue(string propertyName)
        {
            PropertyInfo propInfo = GetType().GetProperty(propertyName);
            return propInfo.GetValue(this);
        }

        protected string GetError(string propertyName)
        {
            string error = string.Empty;
            var value = GetValue(propertyName);
            var results = new List<ValidationResult>(1);
            var result = Validator.TryValidateProperty(
                value,
                new ValidationContext(this)
                {
                    MemberName = propertyName
                },
                results);
            if (!result)
            {
                var validationResult = results.First();
                error = validationResult.ErrorMessage;
            }
            return error;
        }

        protected bool ValidProperty(string propertyName)
        {
            ClearErrors(propertyName);
            string error = GetError(propertyName);
            if (error != string.Empty)
            {
                AddError(propertyName, error);
                return false;
            }
            return true;
        }

        public virtual bool IsValid()
        {
            bool isValid = true;
            PropertyInfo[] properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.GetCustomAttributes(typeof(SkipPropertyAttribute), true).Length == 0).ToArray();

            foreach (PropertyInfo p in properties)
            {
                if (!ValidProperty(p.Name))
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        public List<string> GetErrors()
        {
            List<string> result = new List<string>();
            foreach (var error in _errorsByPropertyName)
            {
                foreach (var item in error.Value)
                {
                    result.Add(item);
                } 
            }

            return result;
        }

    }
}
