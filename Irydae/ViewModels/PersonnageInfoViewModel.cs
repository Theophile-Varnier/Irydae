using System;
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

        private Personnage personnage;

        public Personnage Personnage
        {
            get { return personnage; }
            set
            {
                personnage = value;
                OnPropertyChanged("Personnage");
            }
        }

        public PersonnageInfoViewModel(OptionsService service)
        {
            this.service = service;
            Personnage = new Personnage();
            if (Positions == null)
            {
                Positions = new ObservableCollection<KeyValuePair<string, Position>>(service.PredefinedPositions);
            }
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

        public void TryDelete(DisplayMode mode)
        {
            if (SelectedPeriode != null || SelectedRelation != null)
            {
                ModificationStatusService.Instance.Dirty = true;
            }
            switch (mode)
            {
                case DisplayMode.Rps:
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
                        Personnage.Periodes.Remove(SelectedPeriode);
                        SelectedPeriode = null;
                    }
                    break;
                case DisplayMode.Relations:
                    if (SelectedRelation != null)
                    {
                        Personnage.Relations.Remove(SelectedRelation);
                    }
                    break;
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

        private Partenaire selectedRelation;

        public Partenaire SelectedRelation
        {
            get { return selectedRelation; }
            set
            {
                selectedRelation = value;
                OnPropertyChanged("SelectedRelation");
            }
        }

        public bool AddPeriode(Periode periode, bool update = true)
        {
            VerifierPositionPeriode(periode, update);
            Personnage.Periodes.Add(periode);
            return true;
        }

        public void AddPartenaire(string nom, Groupe? groupe)
        {
            Partenaire partenaire = new Partenaire
            {
                Nom = nom,
                Groupe = groupe
            };
            SelectedRp.Partenaires.Add(partenaire);
            if (Personnage.Relations.All(r => r.Nom != nom))
            {
                Personnage.Relations.Add(partenaire);
            }
            SelectedPartenaire = partenaire;
        }

        public static bool VerifierPositionPeriode(Periode periode, bool update = false)
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
            return periode.Position.X == tryPosition.Value.X && periode.Position.Y == tryPosition.Value.Y;
        }

        public static ObservableCollection<KeyValuePair<string, Position>> Positions { get; private set; }

        public static ObservableCollection<string> PositionsNames
        {
            get
            {
                return new ObservableCollection<string>(Positions.Select(kvp => kvp.Key));
            }
        }

        private List<RpType?> rpTypes;

        public List<RpType?> RpTypes
        {
            get { return rpTypes ?? (rpTypes = new List<RpType?>(Enum.GetValues(typeof(RpType)).Cast<RpType?>())); }
        }
    }
}