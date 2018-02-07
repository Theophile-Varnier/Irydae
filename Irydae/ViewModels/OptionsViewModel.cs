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

        public OptionsViewModel(OptionsService optionsService)
        {
            ResetCommand = new RelayCommand(Reset);
            service = optionsService;
            Options = service.GetOptions();
        }

        public void Save()
        {
            service.SaveOptions(Options);
        }

        public void CancelDialog()
        {
            Options = service.GetOptions();
        }

        public void Reset()
        {
            service.SetDefaultValue(Options);
        }
    }
}