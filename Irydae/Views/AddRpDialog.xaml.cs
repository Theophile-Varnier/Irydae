using System.Windows;
using Irydae.Model;
using Irydae.ViewModels;

namespace Irydae.Views
{
    /// <summary>
    /// Logique d'interaction pour AddRpDialog.xaml
    /// </summary>
    public partial class AddRpDialog : Window
    {
        private PersonnageInfoViewModel ViewModel
        {
            get { return DataContext as PersonnageInfoViewModel; }
        }
        public AddRpDialog()
        {
            InitializeComponent();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddAndContinue(object sender, RoutedEventArgs e)
        {
            AddRp();
        }

        private void ToggleOkButtons(bool toggle)
        {
            AddButton.IsEnabled = toggle;
            AddAnotherButton.IsEnabled = toggle;
            AddPartenaire.IsEnabled = toggle;
        }

        private void AddRp()
        {
            Rp item = new Rp
            {
                Titre = TitreInput.Text,
                Url = UrlInput.Text
            };
            ViewModel.SelectedPeriode.Rps.Add(item);
            ViewModel.SelectedRp = item;
            TitreInput.Text = string.Empty;
            UrlInput.Text = string.Empty;
            ToggleOkButtons(false);
        }

        private void AddAndClose(object sender, RoutedEventArgs e)
        {
            AddRp();
            Close();
        }

        private void AddAndAddPartenaire(object sender, RoutedEventArgs e)
        {
            AddRp();
            AddPartenaireDialog innerDialog = new AddPartenaireDialog();
            innerDialog.Owner = this;
            innerDialog.DataContext = ViewModel;
            innerDialog.ShowDialog();
        }

        private void TitreInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!(string.IsNullOrWhiteSpace(TitreInput.Text) || string.IsNullOrWhiteSpace(UrlInput.Text)))
            {
                ToggleOkButtons(true);
            }
        }
    }
}
