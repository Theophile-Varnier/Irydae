using System;
using System.Linq;
using System.Web.UI.WebControls;
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
            int x = 42;
            int y = 42;
            var samePlace = ViewModel.Periodes.FirstOrDefault(p => p.Lieu == LieuInput.Text);
            if (samePlace != null)
            {
                x = samePlace.Position.X;
                y = samePlace.Position.Y;
            }
            Periode periode = new Periode
            {
                Lieu = LieuInput.Text,
                DateDebut = StartDatePicker.SelectedDate ?? new DateTime(933,1,1),
                DateFin = EndDatePicker.SelectedDate,
                Position = new Position
                {
                    X = x,
                    Y = y
                }
            };
            ViewModel.Periodes.Add(periode);
            ViewModel.SelectedPeriode = periode;
        }

        private void AddAndContinue(object sender, RoutedEventArgs e)
        {
            AddPeriode();
            LieuInput.Text = string.Empty;
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
    }
}
