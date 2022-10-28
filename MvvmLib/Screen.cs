using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MvvmLib
{
    public abstract class Screen : INotifyPropertyChanged
    {
        #region [ VARIABLES ]

        /// <summary>
        /// Window lub UserControl
        /// </summary>
        protected object view;

        private string title;

        private string dialogIdentifier;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region [ COMMANDS ]

        public ICommand OnLoad
        {
            get => RelayCommand.Command((object o) =>
            {
                OnLoadEvent(o);
            });
        }

        public ICommand OnReload
        {
            get => RelayCommand.Command((object o) =>
            {
                OnReloadEvent(o);
            });
        }

        public ICommand WindowClose
        {
            get => RelayCommand.Command(() =>
            {
                ActualWindow.Close();
            });
        }

        public ICommand WindowMinimize
        {
            get => RelayCommand.Command(() =>
            {
                ActualWindow.WindowState = WindowState.Minimized;
            });
        }

        public ICommand WindowMaximize
        {
            get => RelayCommand.Command(() =>
            {
                var window = ActualWindow;
                if (window.WindowState == WindowState.Normal)
                {
                    window.WindowState = WindowState.Maximized;
                }
                else
                {
                    window.WindowState = WindowState.Normal;
                }
            });
        }

        public ICommand Cancel
        {
            get => RelayCommand.Command(() =>
            {
                if (view == null) return;
                if (view is UserControl)
                    DialogHost.CloseDialogCommand.Execute(null, null);
                if (view is Window)
                    (view as Window).Close();
            });
        }

        #endregion

        #region [ METHODS ]

        protected virtual void OnLoadEvent(object o)
        {
        }

        protected virtual void OnReloadEvent(object o)
        {
        }


        public virtual async Task ShowView()
        {
        }

        public virtual void OnFileDrop(string[] filepaths)
        {
            if (filepaths == null) return;
            if (filepaths.Length < 1) return;
        }

        public virtual T GetView<T>()
        {
            return (T)view;
        }

        public virtual dynamic GetView()
        {
            return view;
        }

        /// <summary>
        /// Połączenie zmiennej z oknem
        /// </summary>
        /// <param name="strValue">Nazwa obiektu</param>
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region [ FIELDS ]

        protected string DialogIdentifier
        {
            get => dialogIdentifier;
            set
            {
                dialogIdentifier = value;
                RaisePropertyChanged();
            }
        }

        private Window ActualWindow
        {
            get
            {
                return Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsMouseOver);
            }
        }

        #endregion
    }
}
