using Irydae.Model;
using Irydae.ViewModels;
using System.Windows.Controls;

namespace Irydae.Views
{
    /// <summary>
    /// Logique d'interaction pour PeriodeUserControl.xaml
    /// </summary>
    public partial class PeriodeUserControl : UserControl
    {
        private Periode ViewModel
        {
            get
            {
                return DataContext as Periode;
            }
        }

        public PeriodeUserControl()
        {
            InitializeComponent();
        }

        private void LieuInput_EnterKeyDown(object obj)
        {
            if (ViewModel != null)
            {
                PersonnageInfoViewModel.VerifierPositionPeriode(ViewModel, true);
            }
        }
    }
}
