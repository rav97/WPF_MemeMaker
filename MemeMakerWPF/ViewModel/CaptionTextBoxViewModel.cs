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

        private int leftPos, topPos;

        #endregion

        public CaptionTextBoxViewModel(int number, int initTop, int initLeft)
        {
            CaptionNumber = $"Text {number}";
            LeftPos = initLeft;
            TopPos = initTop;
        }


        #region [ COMMANDS ]

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

        #endregion

        #region [ METHODS ]

        #endregion
    }
}
