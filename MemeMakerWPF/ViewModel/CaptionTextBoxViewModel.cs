using MvvmLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeMakerWPF.ViewModel
{
    public class CaptionTextBoxViewModel : Screen
    {
        #region [ VARIABLES ]

        private string captionText;
        private string captionNumber;

        private int leftPos, topPos, baseCanvasWidth, baseCanvasHeight;

        #endregion

        public CaptionTextBoxViewModel(int number)
        {
            CaptionNumber = $"Text {number}";
            LeftPos = new Random().Next(1, 300);
            TopPos = new Random().Next(1, 350);
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

        public int BaseCanvasWidth
        {
            get => baseCanvasWidth;
            set
            {
                baseCanvasWidth = value;
                RaisePropertyChanged();
            }
        }

        public int BaseCanvasHeight
        {
            get => baseCanvasHeight;
            set
            {
                baseCanvasHeight = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region [ METHODS ]

        #endregion
    }
}
