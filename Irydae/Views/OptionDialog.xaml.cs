using System.Windows;
using Irydae.ViewModels;

namespace Irydae.Views
{
    /// <summary>
    /// Logique d'interaction pour OptionDialog.xaml
    /// </summary>
    public partial class OptionDialog : Window
    {
        public OptionDialog()
        {
            InitializeComponent();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
