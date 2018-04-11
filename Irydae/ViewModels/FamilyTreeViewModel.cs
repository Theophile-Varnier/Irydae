using System.Collections.ObjectModel;

namespace Irydae.ViewModels
{
    public class FamilyTreeViewModel : AbstractPropertyChanged
    {
        public FamilyTreeViewModel()
        {
            Niveaux = new ObservableCollection<TreeLevelViewModel>();
        }

        public int ColumnCount { get; set; }

        public ObservableCollection<TreeLevelViewModel> Niveaux { get; set; } 
    }
}