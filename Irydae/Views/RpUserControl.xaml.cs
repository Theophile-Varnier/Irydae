using Irydae.Model;
using System.Windows.Controls;

namespace Irydae.Views
{
    /// <summary>
    /// Logique d'interaction pour RpUserControl.xaml
    /// </summary>
    public partial class RpUserControl : UserControl
    {
        public RpUserControl()
        {
            InitializeComponent();
        }

        private void TypeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(TypeComboBox.SelectedItem is RpType))
            {
                TypeComboBox.SelectedItem = null;
            }
        }
    }
}
