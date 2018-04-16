using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
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

        public ICommand DeleteTypeRelationCommand { get; private set; }

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
            DeleteTypeRelationCommand = new RelayCommand(o => DeleteTypeRelation((TypeRelation)o));
            service = optionsService;
        }

        public void Save(string profil)
        {
            service.SaveOptions(Options, profil);
        }

        private void DeleteTypeRelation(TypeRelation type)
        {
            Options.TypesRelation.Remove(type);
        }

        public void AddTypeRelation(string name, Color? color)
        {
            if (Options.TypesRelation.All((tr => tr.Nom != name)))
            {
                Options.TypesRelation.Add(new TypeRelation
                {
                    LinkColor = color,
                    Nom = name
                });
            }
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