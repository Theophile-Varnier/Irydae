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
    }
}