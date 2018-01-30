using System.Collections.ObjectModel;
using Irydae.Model;
using Irydae.Services;

namespace Irydae.ViewModels
{
    public class PersonnageInfoViewModel : AbstractPropertyChanged
    {
        private readonly JournalService service;

        public ObservableCollection<Periode> Periodes { get; set; }

        public PersonnageInfoViewModel(JournalService service)
        {
            Periodes = new ObservableCollection<Periode>();
            this.service = service;
        }

        private Periode selectedPeriode;

        public Periode SelectedPeriode
        {
            get { return selectedPeriode; }
            set
            {
                selectedPeriode = value;
                OnPropertyChanged("SelectedPeriode");
            }
        
        }

        private Rp selectedRp;

        public Rp SelectedRp
        {
            get { return selectedRp; }
            set
            {
                selectedRp = value;
                OnPropertyChanged("SelectedRp");
            }
        }

        private Partenaire selectedPartenaire;

        public Partenaire SelectedPartenaire
        {
            get { return selectedPartenaire; }
            set
            {
                selectedPartenaire = value;
                OnPropertyChanged("SelectedPartenaire");
            }
        }
    }
}