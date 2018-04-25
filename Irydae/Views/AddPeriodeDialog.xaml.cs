using System;
using System.Windows;
using Irydae.Model;
using Irydae.ViewModels;

namespace Irydae.Views
{
    /// <summary>
    /// Logique d'interaction pour AddPeriodeDialog.xaml
    /// </summary>
    public partial class AddPeriodeDialog : Window
    {
        private PersonnageInfoViewModel ViewModel
        {
            get { return DataContext as PersonnageInfoViewModel; }
        }

        public Periode Periode { get; private set; }
        public AddPeriodeDialog()
        {
            InitializeComponent();
        }

        private void AddPeriode()
        {
            Periode periode = new Periode
            {
                Lieu = LieuInput.Text,
                DateDebut = StartDatePicker.SelectedDate ?? new DateTime(933, 1, 1),
                DateFin = EndDatePicker.SelectedDate,
                Position = new Position
                {
                    X = 42,
                    Y = 42
                }
            };
            ViewModel.AddPeriode(periode);
            ViewModel.SelectedPeriode = periode;
            LieuInput.Text = string.Empty;
            StartDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
            ToggleOkButtons(false);
        }

        private void AddAndContinue(object sender, RoutedEventArgs e)
        {
            AddPeriode();
        }

        private void AddAndClose(object sender, RoutedEventArgs e)
        {
            AddPeriode();
            Close();
        }

        private void AddAndOpenRpDialog(object sender, RoutedEventArgs e)
        {
            AddPeriode();
            AddRpDialog innerDialog = new AddRpDialog();
            innerDialog.Owner = this;
            innerDialog.DataContext = ViewModel;
            innerDialog.ShowDialog();
        }

        private void ToggleOkButtons(bool toggle)
        {
            AddButton.IsEnabled = toggle;
            AddAnotherButton.IsEnabled = toggle;
            AddAndAddRp.IsEnabled = toggle;
        }

        private void LieuInput_TextChanged(object sender, EventArgs textChangedEventArgs)
        {
            var toggle = !string.IsNullOrWhiteSpace(LieuInput.Text) && StartDatePicker.SelectedDate != null;
            ToggleOkButtons(toggle);
        }
    }
}
