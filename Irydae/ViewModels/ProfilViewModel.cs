using System.Windows.Input;

namespace Irydae.ViewModels
{
    public class ProfilViewModel
    {
        public string Header { get; set; }

        public ICommand Command { get; set; }

        public bool Selected { get; set; }
    }
}