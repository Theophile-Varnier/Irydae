using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Irydae.Model;
using Irydae.Services;

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

        public void SaveDatas()
        {
            journalService.UpdateDatas(CurrentProfile, PersonnageInfo.Periodes);
            ModificationStatusService.Instance.Dirty = false;
        }
    }
}