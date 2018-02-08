using System.Windows.Input;

namespace Irydae.ViewModels
{
    public class ProfilViewModel : AbstractPropertyChanged
    {
        public string Header { get; set; }

        public ICommand Command { get; set; }

        private bool selected;

        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                OnPropertyChanged("Selected");
            }
        }
    }
}