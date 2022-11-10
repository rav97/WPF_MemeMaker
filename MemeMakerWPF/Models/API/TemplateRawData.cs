using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeMakerWPF.Models.API
{
    public class TemplateRawData : TemplateListItem
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
    }
}
