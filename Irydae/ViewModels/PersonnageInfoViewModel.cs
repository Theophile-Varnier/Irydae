using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Irydae.Model;
using Irydae.Services;

namespace Irydae.ViewModels
{
    public class PersonnageInfoViewModel : AbstractPropertyChanged
    {
        private readonly OptionsService service;

        public ObservableCollection<Periode> Periodes { get; set; }

        public PersonnageInfoViewModel(OptionsService service)
        {
            this.service = service;
            Periodes = new ObservableCollection<Periode>();
            Positions = new ObservableCollection<KeyValuePair<string, Position>>(service.PredefinedPositions);
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
            if (SelectedPeriode != null)
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

        public bool AddPeriode(Periode periode, bool update = true)
        {
            VerifierPositionPeriode(periode, update);
            Periodes.Add(periode);
            return true;
        }

        public bool VerifierPositionPeriode(Periode periode, bool update = false)
        {
            // Si on a déjà une période dans le même tierquar on se fait pas chier
            Periode previous = Periodes.FirstOrDefault(p => p.Lieu == periode.Lieu);
            if (previous != null)
            {
                if (update)
                {
                    periode.Position.X = previous.Position.X;
                    periode.Position.Y = previous.Position.Y;
                }
            }
            else
            {
                KeyValuePair<string, Position> tryPosition = Positions.FirstOrDefault(kvp => kvp.Key == periode.Lieu);
                if (tryPosition.Equals(default(KeyValuePair<string, Position>)))
                {
                    // Pas trouvé, on ajoute la période et la clé
                    Positions.Add(new KeyValuePair<string, Position>(periode.Lieu, periode.Position));
                    return true;
                }

                if (update)
                {
                    periode.Position.X = tryPosition.Value.X;
                    periode.Position.Y = tryPosition.Value.Y;
                    return true;
                }

                // On a trouvé le lieu dans les périodes déjà définies
                // On vérifie la cohérence avec un lieu existant
                if (periode.Position.X != tryPosition.Value.X || periode.Position.Y != tryPosition.Value.Y)
                {
                    return false;
                }
            }
            return true;
        }

        public ObservableCollection<KeyValuePair<string, Position>> Positions { get; private set; }
    }
}