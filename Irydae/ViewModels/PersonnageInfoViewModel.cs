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

        public void TryDelete()
        {
            if(SelectedPeriode != null)
            {
                ModificationStatusService.Instance.Dirty = true;
            }
            if (SelectedPartenaire != null)
            {
                SelectedRp.Partenaires.Remove(SelectedPartenaire);
                SelectedPartenaire = null;
            }
            else if (SelectedRp != null)
            {
                SelectedPeriode.Rps.Remove(SelectedRp);
                SelectedRp = null;
            }
            else if (SelectedPeriode != null)
            {
                Periodes.Remove(SelectedPeriode);
                SelectedPeriode = null;
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