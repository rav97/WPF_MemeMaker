using MvvmLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MemeMakerWPF.ViewModel
{
    public class MainWindowViewModel : Screen
    {
        #region [ VARIABLES ]

        private int captionCount = 1, top=0, left=0;

        #endregion

        public MainWindowViewModel()
        {
            CaptionTexts = new ObservableCollection<CaptionTextBoxViewModel>();
        }

        #region [ COMMANDS ]

        public ICommand AddCaption
        {
            get => RelayCommand.Command(() =>
            {
                CaptionTexts.Add(new CaptionTextBoxViewModel(captionCount, top+=50, left+=50));
                captionCount++;
            });
        }

        #endregion

        #region [ PUBLIC ]

        public ObservableCollection<CaptionTextBoxViewModel> CaptionTexts { get; set; }

        #endregion

        #region [ METHODS ]

        #endregion

    }
}
