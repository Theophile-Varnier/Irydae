using Irydae.Helpers;
using Irydae.ViewModels;
using System.Windows;

namespace Irydae.Views
{
    /// <summary>
    /// Logique d'interaction pour YesNoCancelDialog.xaml
    /// </summary>
    public partial class YesNoCancelDialog : Window
    {
        private YesNoCancelDialogViewModel ViewModel
        {
            get
            {
                return DataContext as YesNoCancelDialogViewModel;
            }
        }
        public MessageBoxResult Result { get; private set; }
        public YesNoCancelDialog()
        {
            InitializeComponent();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
            DialogResult = true;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Cancel;
        }

        private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(ViewModel != null)
            {
                Title = ViewModel.Title;
            }
        }

        private void Window_SourceInitialized(object sender, System.EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }
    }
}
