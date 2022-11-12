using MemeMakerWPF.Models.API;
using MemeMakerWPF.Properties;
using MemeMakerWPF.Utility.Apps;
using MemeMakerWPF.Utility.Controls;
using MemeMakerWPF.Utility.Extension;
using MemeMakerWPF.Utility.Managers;
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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MemeMakerWPF.ViewModel
{
    public class MainWindowViewModel : Screen
    {
        #region [ VARIABLES ]

        private int captionCount = 1, top = 0;
        private string templateName;
        private int? usedTemplateId = null;
        private BitmapImage background;
        private Visibility manipulationBoxVisibility = Visibility.Hidden;
        private Size canvasSize, backgroundSize;

        #endregion

        public MainWindowViewModel()
        {
            CaptionTexts = new ObservableCollection<CaptionTextBoxViewModel>();
        }

        protected override void OnLoadEvent(object o)
        {
            LoadFirstImage();
            CalculateRatio(new Size(577.5, 388)); // this will update on image upload or window resize
            top = (int)((CanvasSize.Height - BackgroundSize.Height) / 2) + 5;
            templateName = Path.GetFileNameWithoutExtension(Background.UriSource.ToString());
        }

        #region [ COMMANDS ]

        public ICommand RefreshSizes
        {
            get => RelayCommand.Command((Size size) =>
            {
                if (Background != null)
                {
                    using (new WaitCursor())
                    {
                        CalculateRatio(size);
                    }
                }
            });
        }

        public ICommand SetBackground
        {
            get => RelayCommand.Command(async (Size size) =>
            {
                SetBackgroundViewModel vm = new SetBackgroundViewModel();
                await vm.ShowView();

                using (new WaitCursor())
                {
                    if (vm.Selected)
                    {
                        Background = vm.TemplateImage;
                        templateName = vm.SelectedTemplateName;
                        usedTemplateId = vm?.SelectedTemplate?.Id;
                        CalculateRatio(size);
                        InitFirstCaptions();
                    }
                }
            });
        }

        public ICommand AddCaption
        {
            get => RelayCommand.Command(() =>
            {
                using (new WaitCursor())
                {
                    CaptionTexts.Add(new CaptionTextBoxViewModel(captionCount, top, 15));
                    captionCount++;

                    if (top >= CanvasSize.Height)
                        top = (int)((CanvasSize.Height - BackgroundSize.Height) / 2) + 5;
                    else
                        top += 50;
                }
            });
        }

        public ICommand RemoveCaption
        {
            get => RelayCommand.Command((CaptionTextBoxViewModel vm) =>
            {
                CaptionTexts.Remove(vm);
            });
        }

        public ICommand MouseOverCanvas
        {
            get => RelayCommand.Command(() =>
            {
                ManipulationBoxVisibility = Visibility.Visible;
            });
        }

        public ICommand MouseLeftCanvas
        {
            get => RelayCommand.Command((Size size) =>
            {
                ManipulationBoxVisibility = Visibility.Hidden;

                // to make sure all sizes are up to date
                CalculateRatio(size);
            });
        }

        public ICommand GenerateMeme
        {
            get => RelayCommand.Command(async (Canvas canvas) =>
            {
                try
                {
                    using (new WaitCursor())
                    {
                        CalculateRatio(canvas.RenderSize);
                        var bitmap = BitmapOperations.GetBitmapFromCanvas(canvas);
                        var scaled = BitmapOperations.TryScaleUpImage(bitmap, Background.Width, Background.Height);

                        var savedLocation = BitmapOperations.SavePng(scaled, $"MEME_{templateName}.png");

                        if (savedLocation != null)
                        {
                            await Dialogs.ShowMessage("Your meme should be saved :)");

                            if (usedTemplateId != null)
                                await UploadMeme(usedTemplateId, savedLocation);
                        }
                    }
                }
                catch(Exception e)
                {
                    Dialogs.ShowError(e.Message);
                }
            });
        }

        #endregion

        #region [ PUBLIC ]
        /// <summary>
        /// Collection of VievModels controling caption texts
        /// </summary>
        public ObservableCollection<CaptionTextBoxViewModel> CaptionTexts { get; set; }

        /// <summary>
        /// Source of background image. Containing also oryginal size of image
        /// </summary>
        public BitmapImage Background
        {
            get => background;
            set
            {
                background = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Property bound to visibility of ResizeThumb
        /// </summary>
        public Visibility ManipulationBoxVisibility
        {
            get => manipulationBoxVisibility;
            set
            {
                manipulationBoxVisibility = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Actual size of canvas
        /// </summary>
        public Size CanvasSize
        {
            get => canvasSize;
            set
            {
                canvasSize = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Actual size of background image (background image inside canvas without empty space)
        /// </summary>
        public Size BackgroundSize
        {
            get => backgroundSize;
            set
            {
                backgroundSize = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region [ METHODS ]

        /// <summary>
        /// Load template image from App resources
        /// </summary>
        private void LoadFirstImage()
        {
            try
            {
                BitmapImage image = new BitmapImage(new Uri("pack://application:,,,/Utility/Resources/template.jpg", UriKind.Absolute));
                Background = image;
            }
            catch(Exception e)
            {
                Dialogs.ShowError(e.Message);
            }
        }

        /// <summary>
        /// Creates two caption fields it there is none
        /// </summary>
        private void InitFirstCaptions()
        {
            try
            {
                CaptionTexts.Clear();
                captionCount = 1;
                if (CaptionTexts.Count == 0)
                {
                    CaptionTexts.Add(new CaptionTextBoxViewModel(captionCount, GetCalculatedTopPosition(), 0));
                    captionCount++;
                    CaptionTexts.Add(new CaptionTextBoxViewModel(captionCount, GetCalculatedBottomPosition(), 0));
                    captionCount++;
                }
            }
            catch (Exception e)
            {
                Dialogs.ShowError(e.Message);
            }
        }

        /// <summary>
        /// Calculate position needed to place first caption
        /// </summary>
        /// <returns>Canvas.Top position</returns>
        private int GetCalculatedTopPosition()
        {
            try
            {
                if (CanvasSize == null || BackgroundSize == null)
                    return 0;

                return (int)(CanvasSize.Height - BackgroundSize.Height) / 2;
            }
            catch
            {
                return 20;
            }
        }

        /// <summary>
        /// Calculate position needed to place second caption
        /// </summary>
        /// <returns>Canvas.Top position</returns>
        private int GetCalculatedBottomPosition()
        {
            try
            {
                if (CanvasSize == null || BackgroundSize == null)
                    return 100;

                return GetCalculatedTopPosition() + (int)BackgroundSize.Height - 90;
            }
            catch
            {
                return 50;
            }
        }

        /// <summary>
        /// Calculates CanvasSize and actual background image size
        /// </summary>
        private void CalculateRatio(Size actual)
        {
            try
            {
                CanvasSize = new Size(actual.Width, actual.Height);
                var ratio = Math.Min(actual.Width / Background.Width, actual.Height / Background.Height);
                BackgroundSize = new Size(Background.Width * ratio, Background.Height * ratio);
            }
            catch (Exception e)
            {
                Dialogs.ShowError(e.Message);
            }
        }

        private async Task UploadMeme(int? templateId, string location)
        {
            try
            {
                var result = await Dialogs.ShowQuestion("Would you like to share your meme with community?");

                if (result == MessageBoxResult.Yes)
                {
                    var vm = new UploadImageViewModel(ImageType.Meme, location, templateId);
                    await vm.ShowView();
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
