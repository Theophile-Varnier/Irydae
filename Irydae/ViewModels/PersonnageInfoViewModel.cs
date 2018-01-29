using System.Collections.ObjectModel;
using Irydae.Model;

namespace Irydae.ViewModels
{
    public class PersonnageInfoViewModel : AbstractPropertyChanged
    {
        public ObservableCollection<Periode> Periodes { get; set; }
    }
}