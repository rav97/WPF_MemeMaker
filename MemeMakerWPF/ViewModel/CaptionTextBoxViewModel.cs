using MvvmLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MemeMakerWPF.ViewModel
{
    public class CaptionTextBoxViewModel : Screen
    {
        #region [ VARIABLES ]

        private string captionText;
        private string captionNumber;
        private int fontSize = 20;

        private int leftPos, topPos;

        #endregion

        public CaptionTextBoxViewModel(int number, int initTop, int initLeft)
        {
            CaptionNumber = $"Text {number}";
            LeftPos = initLeft;
            TopPos = initTop;
        }


        #region [ COMMANDS ]

        public ICommand FontSizeUp
        {
            get => RelayCommand.Command(() =>
            {
                if (FontSize < 120)
                    FontSize += 2;
            });
        }

        public ICommand FontSizeDown
        {
            get => RelayCommand.Command(() =>
            {
                if (FontSize > 8)
                    FontSize -= 2;
            });
        }

        #endregion

        #region [ PUBLIC ]

        public string CaptionText
        {
            get => captionText;
            set
            {
                captionText = value;
                RaisePropertyChanged();
            }
        }

        public string CaptionNumber
        {
            get => captionNumber;
            set
            {
                captionNumber = value;
                RaisePropertyChanged();
            }
        }

        public int LeftPos
        {
            get => leftPos;
            set
            {
                leftPos = value;
                RaisePropertyChanged();
            }
        }

        public int TopPos
        {
            get => topPos;
            set
            {
                topPos = value;
                RaisePropertyChanged();
            }
        }

        public int FontSize
        {
            get => fontSize;
            set
            {
                fontSize = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region [ METHODS ]

        #endregion
    }
}
