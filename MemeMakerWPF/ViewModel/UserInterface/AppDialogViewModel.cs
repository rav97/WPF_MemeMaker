using MaterialDesignThemes.Wpf;
using MemeMakerWPF.View.UserInterface;
using MvvmLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MemeMakerWPF.ViewModel.UserInterface
{
    public class AppDialogViewModel : Screen
    {
        #region [ VARIABLES ]

        private Visibility isMessage;
        private Visibility isQuestion;
        private string message;
        private UserControl controlView;
        private MessageBoxResult result;

        #endregion

        public AppDialogViewModel(string message, bool? question = false)
        {
            Mouse.OverrideCursor = null;
            if(question != null)
            {
                this.IsQuestion = question.Value ? Visibility.Visible : Visibility.Collapsed;
                this.IsMessage = question.Value ? Visibility.Collapsed : Visibility.Visible;
            }
            else
            {
                this.IsQuestion = Visibility.Collapsed;
                this.IsMessage = Visibility.Collapsed;
            }
            this.Message = message;
        }


        #region [ COMMANDS ]

        public ICommand YesCommand
        {
            get => RelayCommand.Command(() =>
            {
                this.Result = MessageBoxResult.Yes;
                DialogHost.CloseDialogCommand.Execute(null, null);
            });
        }

        public ICommand CancelCommand
        {
            get => RelayCommand.Command(() =>
            {
                this.Result = MessageBoxResult.Cancel;
                DialogHost.CloseDialogCommand.Execute(null, null);
            });
        }

        public ICommand NoCommand
        {
            get => RelayCommand.Command(() =>
            {
                this.Result = MessageBoxResult.No;
                DialogHost.CloseDialogCommand.Execute(null, null);
            });
        }

        public ICommand OkCommand
        {
            get => RelayCommand.Command(() =>
            {
                this.Result = MessageBoxResult.OK;
                DialogHost.CloseDialogCommand.Execute(null, null);
            });
        }

        #endregion


        #region [ PUBLIC ]

        public Visibility IsMessage
        {
            get => isMessage;
            set
            {
                isMessage = value;
                RaisePropertyChanged();
            }
        }
        public Visibility IsQuestion
        {
            get => isQuestion;
            set
            {
                isQuestion = value;
                RaisePropertyChanged();
            }
        }
        public string Message
        {
            get => message;
            set
            {
                message = value;
                RaisePropertyChanged();
            }
        }
        public UserControl ControlView
        {
            get
            {
                if(controlView == null)
                {
                    controlView = new AppDialogView();
                    controlView.DataContext = this;
                }
                return controlView;
            }
            set
            {
                controlView = value;
                RaisePropertyChanged();
            }
        }
        public MessageBoxResult Result
        {
            get => result;
            set
            {
                result = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region [ METHODS ]

        #endregion

    }
}
