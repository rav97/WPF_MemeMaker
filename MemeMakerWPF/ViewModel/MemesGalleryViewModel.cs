using MemeMakerWPF.Models.API;
using MemeMakerWPF.Utility.Apps;
using MemeMakerWPF.Utility.Controls;
using MemeMakerWPF.Utility.Extension;
using MemeMakerWPF.Utility.Managers;
using MemeMakerWPF.View;
using MvvmLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MemeMakerWPF.ViewModel
{
    public class MemesGalleryViewModel : Screen
    {
        #region [ VARIABLES ]

        private int skip = 0, take = 5;
        private readonly ApiMemeManager apiMemeManager;

        private MemeRawData selectedMeme;
        private BitmapImage selectedImage;
        private bool leftEnabled, rightEnabled, downloadEnabled;
        public ObservableCollection<MemeRawData> MemesList { get; set; }

        #endregion

        public MemesGalleryViewModel()
        {
            apiMemeManager = new ApiMemeManager();
            MemesList = new ObservableCollection<MemeRawData>();
        }

        public override async Task ShowView()
        {
            view = new MemesGalleryView();
            (view as MemesGalleryView).DialogIdentifier = $"MemesGallery{DateTime.Now.Ticks}";
            (view as MemesGalleryView).DataContext = this;
            (view as MemesGalleryView).Show();
        }
        protected override void OnLoadEvent(object o)
        {
            using (new WaitCursor())
            {
                LeftEnabled = false;
                RightEnabled = true;
                DownloadEnabled = false;
                FillListView();
            }
        }

        #region [ COMMANDS ]

        public ICommand Left
        {
            get => RelayCommand.Command(() =>
            {
                using (new WaitCursor())
                {
                    if (skip >= take)
                    {
                        skip -= take;
                        FillListView();
                        RightEnabled = true;
                    }
                    else
                    {
                        skip = 0;
                        FillListView();
                        LeftEnabled = false;
                    }
                }
            });
        }

        public ICommand Right
        {
            get => RelayCommand.Command(() =>
            {
                using (new WaitCursor())
                {
                    skip += take;
                    LeftEnabled = true;
                    FillListView();

                    if (MemesList.Count == 0)
                    {
                        Left.Execute(null);
                        RightEnabled = false;
                    }
                }
            });
        }

        public ICommand Download
        {
            get => RelayCommand.Command(async () =>
            {
                using (new WaitCursor())
                {
                    Bitmap bitmap = SelectedImage.ToBitmap();
                    var savedLocation = BitmapOperations.SavePng(bitmap, SelectedMeme.Path);

                    if (savedLocation != null)
                        await Dialogs.ShowMessage("Meme should be saved");
                }
            });
        }

        #endregion

        #region [ PUBLIC ]

        public MemeRawData SelectedMeme
        {
            get => selectedMeme;
            set
            {
                selectedMeme = value;

                if (value == null)
                {
                    SelectedImage = null;
                    DownloadEnabled = false;
                }
                else
                {
                    SelectedImage = value.Image;
                    DownloadEnabled = true;
                }

                RaisePropertyChanged();
            }
        }

        public BitmapImage SelectedImage
        {
            get => selectedImage;
            set
            {
                selectedImage = value;
                RaisePropertyChanged();
            }
        }

        public bool LeftEnabled
        {
            get => leftEnabled;
            set
            {
                leftEnabled = value;
                RaisePropertyChanged();
            }
        }
        public bool RightEnabled
        {
            get => rightEnabled;
            set
            {
                rightEnabled = value;
                RaisePropertyChanged();
            }
        }

        public bool DownloadEnabled
        {
            get => downloadEnabled;
            set
            {
                downloadEnabled = value;
                RaisePropertyChanged();
            }
        }


        #endregion

        #region [ METHODS ]

        private void FillListView()
        {
            var list = apiMemeManager.GetRecentMemes(skip, take);
            if (list != null)
            {
                MemesList.Clear();
                foreach (var m in list)
                    MemesList.Add(m);
            }
            
        }

        #endregion
    }
}
