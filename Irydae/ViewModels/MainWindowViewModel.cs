using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Irydae.Model;
using Irydae.Services;
using WPFCustomMessageBox;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

namespace Irydae.ViewModels
{
    public class MainWindowViewModel : AbstractPropertyChanged
    {
        private readonly JournalService journalService;

        private const string CurrentProfilePropertyName = "CurrentProfile";

        private string currentProfile;

        public PersonnageInfoViewModel PersonnageInfo { get; private set; }

        public string CurrentProfile
        {
            get { return currentProfile; }
            set
            {
                currentProfile = value;
                OnPropertyChanged(CurrentProfilePropertyName);
            }
        }

        public MainWindowViewModel(JournalService service)
        {
            journalService = service;
            PersonnageInfo = new PersonnageInfoViewModel(service);
            PropertyChanged += OnPropertyChanged;
            CurrentProfile = "data";
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            switch (propertyChangedEventArgs.PropertyName)
            {
                case CurrentProfilePropertyName:
                    IEnumerable<Periode> periodes = journalService.ParseDatas(CurrentProfile);
                    if (periodes != null)
                    {
                        PersonnageInfo.Periodes = new ObservableCollection<Periode>(periodes);
                    }
                    break;
            }
        }

        public bool CheckModifications()
        {
            if (ModificationStatusService.Instance.Dirty)
            {
                MessageBoxResult messageResult = CustomMessageBox.ShowYesNoCancel("Si tu quittes cette application sans avoir sauvegardé ces épuisantes modifications (Ctrl + S), le monde risque de s'effondrer et les anomalies régneront sans partage sur Irydaë. Et aussi va falloir recommencer.\n\nTu veux que je sauvegarde pour toi (ça fera 5€) ?", "Attention malheureux !", "C'est fort aimable.", "5 balles ?! Crève.", "Tout bien réfléchi...");
                switch (messageResult)
                {
                    case MessageBoxResult.Yes:
                        SaveDatas();
                        return true;
                    case MessageBoxResult.No:
                        MessageBoxResult innerResult = CustomMessageBox.ShowYesNoCancel("Et pour 2€ ?", "Allez s'teup !", "Ok, ok, sauvegarde.", "Non mais vraiment.", "Attends, j'ai oublié un truc.");
                        switch (innerResult)
                        {
                            case MessageBoxResult.Yes:
                                SaveDatas();
                                return true;
                            case MessageBoxResult.No:
                                MessageBoxResult innerInnerResult = CustomMessageBox.ShowYesNoCancel("Allez, comme c'est toi je le fais gratuitement.", "Comme ça radine !", "Ah bah quand même.", "En fait je voulais vraiment pas sauvegarder.", "Ca m'a fait penser à un truc.");
                                switch (innerInnerResult)
                                {
                                    case MessageBoxResult.Yes:
                                        SaveDatas();
                                        return true;
                                    case MessageBoxResult.No:
                                        return true;
                                    default:
                                        return false;
                                }
                            default:
                                return false;
                        }
                    default:
                        return false;
                }
            }
            return true;
        }

        public void SaveDatas()
        {
            journalService.UpdateDatas(CurrentProfile, PersonnageInfo.Periodes);
            ModificationStatusService.Instance.Dirty = false;
        }
    }
}