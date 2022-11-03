using Microsoft.Win32;
using MvvmLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MemeMakerWPF.ViewModel
{
    public class MainWindowViewModel : Screen
    {
        #region [ VARIABLES ]

        private int captionCount = 1, top=0, left=0;
        private BitmapImage background;
        private Visibility manipulationBoxVisibility = Visibility.Hidden;
        private Size canvasSize, backgroundSize;

        #endregion

        public MainWindowViewModel()
        {
            CaptionTexts = new ObservableCollection<CaptionTextBoxViewModel>();
        }

        protected override void OnLoadEvent(object o)
        {

        }

        #region [ COMMANDS ]

        public ICommand SetBackground
        {
            get => RelayCommand.Command((Size size) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        Background = new BitmapImage(new Uri(openFileDialog.FileName));
                        CanvasSize = new Size(size.Width, size.Height);
                        CalculateRatio(size);
                        InitFirstCaptions();
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

        public ICommand MouseOverCanvas
        {
            get => RelayCommand.Command(() =>
            {
                ManipulationBoxVisibility = Visibility.Visible;
            });
        }

        public ICommand MouseLeftCanvas
        {
            get => RelayCommand.Command(() =>
            {
                ManipulationBoxVisibility = Visibility.Hidden;
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

        public Visibility ManipulationBoxVisibility
        {
            get => manipulationBoxVisibility;
            set
            {
                manipulationBoxVisibility = value;
                RaisePropertyChanged();
            }
        }

        public Size CanvasSize
        {
            get => canvasSize;
            set
            {
                canvasSize = value;
                RaisePropertyChanged();
            }
        }

        public Size BackgroundSize
        {
            get => backgroundSize;
            set
            {
                backgroundSize = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region [ METHODS ]

        private void InitFirstCaptions()
        {
            CaptionTexts.Add(new CaptionTextBoxViewModel(captionCount, (int)(CanvasSize.Height - BackgroundSize.Height) / 2, 200));
            captionCount++;
            CaptionTexts.Add(new CaptionTextBoxViewModel(captionCount, ((int)(CanvasSize.Height - BackgroundSize.Height) / 2) + (int)BackgroundSize.Height - 90, 200));
            captionCount++;
        }

        private void CalculateRatio(Size actual)
        {
            var ratio = Math.Min(actual.Width / Background.Width, actual.Height / Background.Height);
            BackgroundSize = new Size(Background.Width * ratio, Background.Height * ratio);
        }

        #endregion

    }
}
