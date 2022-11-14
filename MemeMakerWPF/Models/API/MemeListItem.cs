using MvvmLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeMakerWPF.Models.API
{
    public class MemeListItem : BindModel
    {
        private int id;
        private DateTime? createDate;
        private string name;
        private string path;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public DateTime? CreateDate
        {
            get => createDate;
            set
            {
                createDate = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Path
        {
            get => path;
            set
            {
                path = value;
                OnPropertyChanged();
            }
        }
    }
}
