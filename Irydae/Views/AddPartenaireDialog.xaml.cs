using System.Windows;
using Irydae.Helpers;
using Irydae.Model;
using Irydae.ViewModels;

namespace Irydae.Views
{
    /// <summary>
    /// Logique d'interaction pour AddPartenaireDialog.xaml
    /// </summary>
    public partial class AddPartenaireDialog : Window
    {
        private PersonnageInfoViewModel ViewModel
        {
            get { return DataContext as PersonnageInfoViewModel; }
        }

        public AddPartenaireDialog()
        {
            InitializeComponent();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddAndContinue(object sender, RoutedEventArgs e)
        {
            AddPartenaire();
            NameInput.Text = string.Empty;
        }

        private void AddPartenaire()
        {
            Groupe? groupe = GroupeComboBox.SelectedItem as Groupe?;
            Partenaire partenaire = new Partenaire
            {
                Nom = NameInput.Text,
                Groupe = groupe
            };
            ViewModel.SelectedRp.Partenaires.Add(partenaire);
            var relation = ViewModel.Personnage.Relations.GetOrAddNew(r => r.Nom == partenaire.Nom, () => partenaire);
            relation.AjouterRpCommun(ViewModel.SelectedRp);
            ViewModel.SelectedPartenaire = partenaire;
        }

        private void AddAndClose(object sender, RoutedEventArgs e)
        {
            AddPartenaire();
            Close();
        }
    }
}
