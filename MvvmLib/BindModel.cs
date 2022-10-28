using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLib
{
    public class BindModel : ValidModel, INotifyPropertyChanged
    {
        #region [ VARIABLES ]

        private bool changed = true;
        protected Action action;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region [ PUBLIC ]

        public void SetAction(Action action)
        {
            this.action = action;
        }

        /// <summary>
        /// Zwraca informację czy model uległ zmianie od czasu ostatniego sprawdzenia. Odczytanie tej właściwości (nawet w debuggerze) resetuje jej stan.
        /// </summary>
        public bool Changed
        {
            get
            {
                var pom = changed;
                changed = false;
                return pom;
            }
            set
            {
                changed = value;
            }
        }

        #endregion

        #region [ PRIATE ]

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                ValidProperty(propertyName);
                handler(this, new PropertyChangedEventArgs(propertyName));
                Changed = true;
            }

            if (action != null)
                action();
        }

        #endregion

    }
}
