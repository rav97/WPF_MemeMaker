using MemeMakerWPF.Utility.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MemeMakerWPF.Models.API
{
    public class MemeRawData : MemeListItem
    {
        private byte[] imageData;

        public byte[] ImageData
        {
            get => imageData;
            set
            {
                imageData = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage Image
        {
            get
            {
                return this.GetBitmap();
            }
        }
    }
}
