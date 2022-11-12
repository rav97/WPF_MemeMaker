using MemeMakerWPF.Models;
using MemeMakerWPF.Models.API;
using MemeMakerWPF.Properties;
using MemeMakerWPF.Utility.Apps;
using MemeMakerWPF.Utility.Controls;
using MemeMakerWPF.Utility.Extension;
using MemeMakerWPF.Utility.Managers;
using MemeMakerWPF.View;
using Microsoft.Win32;
using MvvmLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MemeMakerWPF.ViewModel
{
    public class SetBackgroundViewModel : Screen
    {
        #region [ VARIABLES ]

        private BitmapImage _templateImage;
        private string _templateSearch;
        private bool isLocalTemplate = false;
        private bool selected = false;
        private string selectedTemplateName;

        private TemplateListItem selectedTemplate;

        private readonly ApiTemplateManager apiTemplateManager;

        public ObservableCollection<TemplateListItem> TemplateList { get; set; }

        #endregion

        public SetBackgroundViewModel()
        {
            apiTemplateManager = new ApiTemplateManager();
            TemplateList = new ObservableCollection<TemplateListItem>();
        }

        public override async Task ShowView()
        {
            view = new SetBackgroundView();
            (view as SetBackgroundView).DataContext = this;
            await Dialogs.ShowDialog(view);
        }

        protected override void OnLoadEvent(object o)
        {
            using (new WaitCursor())
            {
                InitTemplateList();
            }
        }

        #region [ COMMANDS ]

        public ICommand SearchLocally
        {
            get => RelayCommand.Command(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        if (Settings.Default.ALLOWED_EXTENSIONS.Contains(Path.GetExtension(openFileDialog.FileName)))
                        {
                            TemplateImage = new BitmapImage(new Uri(openFileDialog.FileName));
                            IsLocalTemplate = true;
                            SelectedTemplateName = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                        }
                        else
                        {
                            Dialogs.ShowError("Invalid image extension");
                        }
                    }
                    catch { }
                }
            });
        }

        public ICommand SearchOnline
        {
            get => RelayCommand.Command((string textSearch) =>
            {
                using (new WaitCursor())
                {
                    if (string.IsNullOrWhiteSpace(textSearch))
                        InitTemplateList();
                    else
                    {
                        var list = apiTemplateManager.SearchForTemplate(textSearch);
                        FillTemplateList(list);
                    }
                }
            });
        }

        public ICommand UploadTemplate
        {
            get => RelayCommand.Command(async () =>
            {
                var vm = new UploadImageViewModel(ImageType.Template, TemplateImage.UriSource.AbsolutePath);
                await vm.ShowView();

                IsLocalTemplate = !vm.Result;
            });
        }

        public ICommand SelectBackground
        {
            get => RelayCommand.Command(() =>
            {
                using (new WaitCursor())
                {
                    if (!IsLocalTemplate)
                    {
                        if (SelectedTemplate != null)
                            apiTemplateManager.SaveUsage(SelectedTemplate.Id);
                    }

                    Selected = true;
                }
                Dialogs.Close();
            });
        }


        #endregion

        #region [ PUBLIC ]

        public BitmapImage TemplateImage
        {
            get => _templateImage;
            set
            {
                _templateImage = value;
                RaisePropertyChanged();
            }
        }

        public string TemplateSearch
        {
            get => _templateSearch;
            set
            {
                _templateSearch = value;
                RaisePropertyChanged();
            }
        }

        public TemplateListItem SelectedTemplate
        {
            get => selectedTemplate;
            set
            {
                selectedTemplate = value;
                RaisePropertyChanged();

                IsLocalTemplate = false;
                DownloadSelectedTemplate(value);
            }
        }

        public bool IsLocalTemplate
        {
            get => isLocalTemplate;
            set
            {
                isLocalTemplate = value;
                RaisePropertyChanged();
            }
        }

        public bool Selected
        {
            get => selected;
            set
            {
                selected = value;
                RaisePropertyChanged();
            }
        }

        public string SelectedTemplateName
        {
            get => selectedTemplateName;
            set
            {
                selectedTemplateName = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region [ METHODS ]

        private void InitTemplateList()
        {
            try
            {
                var list = apiTemplateManager.GetPopularTemplates(20);
                FillTemplateList(list);
            }
            catch(Exception e)
            {
                Dialogs.ShowError(e.Message);
            }
        }

        private void FillTemplateList(IEnumerable<TemplateListItem> list)
        {
            try
            {
                TemplateList.Clear();
                foreach (var e in list)
                    TemplateList.Add(e);
            }
            catch (Exception e)
            {
                Dialogs.ShowError(e.Message);
            }
        }

        private void DownloadSelectedTemplate(TemplateListItem template)
        {
            try
            {
                if (template != null)
                {
                    var data = apiTemplateManager.GetTemplateById(template.Id);

                    var bitmap = data.GetBitmap();
                    TemplateImage = bitmap;
                    SelectedTemplateName = Path.GetFileNameWithoutExtension(template.Path);
                }
                else
                {
                    TemplateImage = null;
                    SelectedTemplateName = null;
                }
            }
            catch (Exception e)
            {
                Dialogs.ShowError(e.Message);
            }
        }

        #endregion
    }
}
