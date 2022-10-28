using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MvvmLib
{
    public class RelayCommand : ICommand
    {
        #region [ VARIABLES ]

        readonly Action<object> m_execute;
        readonly Predicate<object> m_canExecute;

        #endregion

        #region [ PUBLIC ]

        public RelayCommand(Action<object> execute)
           : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("Execute");
            m_execute = execute;
            m_canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return m_canExecute == null ? true : m_canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            m_execute(parameter);
        }

        #endregion

        #region [ STATIC ]

        public static ICommand Command<T>(Action<T> action = null)
        {
            try
            {
                RelayCommand icomand = new RelayCommand
                (
                    param =>
                    {
                        action((T)param);
                    },
                    param => true
                );
                return icomand;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    
        public static ICommand Command(Action<object> action)
        {
            try
            {
                RelayCommand icomand = new RelayCommand
                (
                    param =>
                    {
                        action(param);
                    },
                    param => true
                );
                return icomand;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static ICommand Command(Action action)
        {
            try
            {
                RelayCommand icomand = new RelayCommand
                (
                    param =>
                    {
                        action();
                    },
                    param => true
                );
                return icomand;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        #endregion
    }
}
