using MemeMakerWPF.Models.API;
using MemeMakerWPF.Utility.Apps;
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
        private int? templateId;
        private ApiTemplateManager apiTemplateManager;
        private ApiMemeManager apiMemeManager;

        private UploadImageData uploadImageData;
        
        public bool Result { get; set; }

        #endregion

        public UploadImageViewModel(ImageType type, string imagePath, int? templateId = null)
        {
            using (new WaitCursor())
            {
                imageType = type;
                this.templateId = templateId;
                apiTemplateManager = new ApiTemplateManager();
                apiMemeManager = new ApiMemeManager();

                UploadImageData = new UploadImageData(null, imagePath);
            }
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
                if (!UploadImageData.IsValid()) return;

                Result = UploadData();

                if (Result)
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

        private bool UploadData()
        {
            try
            {
                using (new WaitCursor())
                {
                    if (imageType == ImageType.Template)
                        return apiTemplateManager.UploadTemplate(UploadImageData);
                    else
                        return apiMemeManager.UploadMeme(templateId.Value, UploadImageData);
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
