using MvvmLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeMakerWPF.Models.API
{
    public class UploadImageData : BindModel
    {
        private string imageName;
        private FileUploadModel file;

        public UploadImageData(string name, string filePath)
        {
            ImageName = name;
            File = new FileUploadModel(filePath);
        }

        [Required(ErrorMessage = "Image name is required")]
        [MinLength(5, ErrorMessage = "Image name must count at least 5 characters")]
        [MaxLength(100, ErrorMessage = "Image name can't be longer than 100 characters")]
        public string ImageName
        {
            get => imageName;
            set
            {
                imageName = value;
                OnPropertyChanged();
            }
        }
        public FileUploadModel File
        {
            get => file;
            set
            {
                file = value;
                OnPropertyChanged();
            }
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}
