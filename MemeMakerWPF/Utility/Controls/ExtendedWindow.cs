using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace MemeMakerWPF.Utility.Controls
{
    public class ExtendedWindow : Window, INotifyPropertyChanged
    {
        public string DialogIdentifier
        {
            get { return (string)GetValue(DialogIdentifierProperty); }
            set { SetValue(DialogIdentifierProperty, value); }
        }

        public static readonly DependencyProperty DialogIdentifierProperty =
            DependencyProperty.Register(nameof(DialogIdentifier), typeof(string), typeof(ExtendedWindow), new FrameworkPropertyMetadata(string.Empty));

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }

        public ExtendedWindow()
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.Loaded += ExtendedWindow_Loaded;
            this.KeyDown += ExtendedWindow_KeyDown;
        }

        private void ExtendedWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.F11)
            {
                if (this.WindowState == WindowState.Normal)
                    this.WindowState = WindowState.Maximized;
                else if (this.WindowState == WindowState.Maximized)
                    this.WindowState = WindowState.Normal;

                e.Handled = true;
            }
        }

        //By doing this there is no need to manually add DialogHost to every view
        private void ExtendedWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Panel container;

            //sometimes there is need to specify ContentPanel (it its grid it can not have any Rows or Columns)
            container = (Panel)this.FindName("ContentPanel");

            if (container == null)
                container = (Panel)this.Content;

            container.Children.Add(GetDialogHost());
        }

        private DialogHost GetDialogHost()
        {
            DialogHost dialog = new DialogHost();
            dialog.Identifier = DialogIdentifier;

            Panel.SetZIndex(dialog, 100);
            TransitionAssist.SetDisableTransitions(dialog, true);
            return dialog;
        }
    }
}
