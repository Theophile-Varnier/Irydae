using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Irydae.ViewModels;

namespace Irydae.Views
{
    /// <summary>
    /// Logique d'interaction pour AddProfile.xaml
    /// </summary>
    public partial class AddProfileDialog : Window
    {
        private ProfilViewModel ViewModel
        {
            get { return DataContext as ProfilViewModel; }
        }

        public AddProfileDialog()
        {
            InitializeComponent();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
        }

        private void AddAndClose(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
