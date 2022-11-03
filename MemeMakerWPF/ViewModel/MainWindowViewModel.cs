using Microsoft.Win32;
using MvvmLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MemeMakerWPF.ViewModel
{
    public class MainWindowViewModel : Screen
    {
        #region [ VARIABLES ]

        private int captionCount = 1, top=0, left=0;
        private BitmapImage background;

        #endregion

        public MainWindowViewModel()
        {
            CaptionTexts = new ObservableCollection<CaptionTextBoxViewModel>();
        }

        #region [ COMMANDS ]

        public ICommand SetBackground
        {
            get => RelayCommand.Command(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        Background = new BitmapImage(new Uri(openFileDialog.FileName));
                    }
                    catch { }
                }
            });
        }

        public ICommand AddCaption
        {
            get => RelayCommand.Command(() =>
            {
                CaptionTexts.Add(new CaptionTextBoxViewModel(captionCount, top+=50, left+=50));
                captionCount++;
            });
        }

        public ICommand RemoveCaption
        {
            get => RelayCommand.Command((CaptionTextBoxViewModel vm) =>
            {
                CaptionTexts.Remove(vm);
            });
        }

        #endregion

        #region [ PUBLIC ]

        public ObservableCollection<CaptionTextBoxViewModel> CaptionTexts { get; set; }
        public BitmapImage Background
        {
            get => background;
            set
            {
                background = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region [ METHODS ]

        #endregion

    }
}
