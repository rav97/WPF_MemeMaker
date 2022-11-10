using MemeMakerWPF.Models.API;
using MemeMakerWPF.Utility.Controls;
using MemeMakerWPF.Utility.Managers;
using MemeMakerWPF.View;
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
    public enum ImageType
    {
        Template,
        Meme
    }

    public class UploadImageViewModel : Screen
    {
        #region [ VARIABLES ]

        private ImageType imageType;
        private ApiTemplateManager apiTemplateManager;

        private UploadImageData uploadImageData;
        
        #endregion

        public UploadImageViewModel(ImageType type, string imagePath)
        {
            imageType = type;
            apiTemplateManager = new ApiTemplateManager();

            UploadImageData = new UploadImageData(null, imagePath);
        }

        public override async Task ShowView()
        {
            view = new UploadImageView();
            (view as UploadImageView).DataContext = this;
            await Dialogs.ShowDialog(view);
        }


        #region [ COMMANDS ]

        public ICommand Upload
        {
            get => RelayCommand.Command(() =>
            {
                bool result;

                if (!UploadImageData.IsValid()) return;

                if (imageType == ImageType.Template)
                    result = apiTemplateManager.UploadTemplate(UploadImageData);
                else
                    result = false;

                if (result)
                    Dialogs.Close();
                else
                    Dialogs.ShowError("Image upload failed. Try again later.");

            });
        }

        #endregion

        #region [ PUBLIC ]

        public UploadImageData UploadImageData
        {
            get => uploadImageData;
            set
            {
                uploadImageData = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region [ METHODS ]

        #endregion
    }
}
