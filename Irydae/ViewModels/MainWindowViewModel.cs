using Irydae.Services;

namespace Irydae.ViewModels
{
    public class MainWindowViewModel : AbstractPropertyChanged
    {
        private readonly JournalService journalService;

        private string currentProfile;

        public string CurrentProfile
        {
            get { return currentProfile; }
            set
            {
                currentProfile = value;
                OnPropertyChanged("CurrentProfile");
            }
        }

        public MainWindowViewModel(JournalService service)
        {
            journalService = service;
        }
    }
}