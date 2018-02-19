using System.Windows.Input;
using Irydae.Helpers;
using Irydae.Model;
using Irydae.Services;

namespace Irydae.ViewModels
{
    public class OptionsViewModel : AbstractPropertyChanged
    {
        private readonly OptionsService service;
        private Options options;

        public ICommand ResetCommand { get; private set; }

        public Options Options
        {
            get { return options; }
            set
            {
                options = value;
                OnPropertyChanged("Options");
            }
        }

        public void Load(string profil)
        {
            Options = service.GetOptions(profil);
        }

        public OptionsViewModel(OptionsService optionsService)
        {
            ResetCommand = new RelayCommand(Reset);
            service = optionsService;
        }

        public void Save(string profil)
        {
            service.SaveOptions(Options, profil);
        }

        public void CancelDialog(string current)
        {
            Options = service.GetOptions(current);
        }

        public void Reset()
        {
            service.SetDefaultValue(Options);
        }
    }
}