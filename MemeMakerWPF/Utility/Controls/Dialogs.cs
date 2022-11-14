using MaterialDesignThemes.Wpf;
using MemeMakerWPF.View.UserInterface;
using MemeMakerWPF.ViewModel.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MemeMakerWPF.Utility.Controls
{
    public class Dialogs
    {
        private const string ROOT_DIALOG = "RootDialog";

        public static async Task ShowMessage(string message)
        {
            try
            {
                Mouse.OverrideCursor = null;
                var vm = new AppDialogViewModel(message);
                await DialogHost.Show(vm.ControlView, Identifier());
            }
            catch {
            }
        }

        public static async void ShowError(string message = "Unidentified error occured")
        {
            try
            {
                Mouse.OverrideCursor = null;
                var viewModel = new AppDialogViewModel(message);
                await DialogHost.Show(viewModel.ControlView, Identifier());
            }
            catch
            {

            }
        }

        public static async Task<MessageBoxResult> ShowQuestion(string message)
        {
            try
            {
                Mouse.OverrideCursor = null;
                var viewModel = new AppDialogViewModel(message, true);
                await DialogHost.Show(viewModel.ControlView, Identifier());
                return viewModel.Result;

            }
            catch
            {
                return MessageBoxResult.None;
            }
        }

        public static async Task ShowDialog(object view)
        {
            try
            {
                if (view is UserControl)
                {

                    DialogView dialogView = new DialogView();
                    dialogView.DialogObject.Identifier = DateTime.Now.ToString("yyyyMMddHHmmss");
                    dialogView.ContentObject.Content = view;
                    await DialogHost.Show(dialogView, Identifier());
                }
                else
                {
                    (view as Window).ShowDialog();
                }

            }
            catch (Exception)
            {
            }
        }

        public static void Close()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }


        private static string Identifier()
        {
            ExtendedWindow temp = Application.Current.Windows.OfType<ExtendedWindow>().SingleOrDefault(w => w.IsMouseOver);
            if (temp == null)
                temp = Application.Current.Windows.OfType<ExtendedWindow>().Last();

            if(temp != null)
            {
                var dialog = FindDialog(temp);
                if(dialog != null)
                {
                    if (dialog.IsOpen)
                        return FindLastIdentifier(dialog);
                    else
                        return temp.DialogIdentifier ?? ROOT_DIALOG;
                }
            }
            return ROOT_DIALOG;
        }

        private static DialogHost FindDialog(ExtendedWindow window)
        {
            Panel mainContainter = (Panel)window.Content;
            UIElementCollection element = mainContainter.Children;
            foreach(Control dialog in FindVisualChildren<Control>(window))
            {
                if (dialog is DialogHost)
                    return (dialog as DialogHost);
            }

            return null;
        }

        private static string FindLastIdentifier(DialogHost dialogHost)
        {
            if (dialogHost.DialogContent == null)
                return dialogHost.Identifier.ToString();
            if (dialogHost.DialogContent is AppDialogView)
                return dialogHost.Identifier.ToString();

            DialogView control = (dialogHost.DialogContent as DialogView);
            DialogHost dialog = (control?.Content as DialogHost);

            var temp = dialog?.DialogContent;
            if (temp != null && dialog.IsOpen)
            {
                if (temp is DialogView)
                {
                    var subContent = ((DialogView)temp).Content;
                    if (subContent is DialogHost)
                    {
                        return FindLastIdentifier(subContent as DialogHost);
                    }
                }
            }
            return dialog?.Identifier?.ToString() ?? ROOT_DIALOG;
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

    }
}
