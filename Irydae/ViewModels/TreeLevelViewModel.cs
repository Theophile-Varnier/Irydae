using System.Collections.ObjectModel;
using Irydae.Model;

namespace Irydae.ViewModels
{
    public class TreeLevelViewModel : AbstractPropertyChanged
    {
        public TreeLevelViewModel()
        {
            Membres = new ObservableCollection<Partenaire>();
        }
        private int level;

        public int Level
        {
            get { return level; }
            set
            {
                level = value;
                OnPropertyChanged("Level");
            }
        }

        public ObservableCollection<Partenaire> Membres { get; set; } 
    }
}