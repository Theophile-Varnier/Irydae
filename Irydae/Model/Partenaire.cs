using Irydae.ViewModels;
using Newtonsoft.Json;

namespace Irydae.Model
{
    [JsonObject]
    public class Partenaire : AbstractPropertyChanged
    {
        [JsonProperty] 
        private string nom;

        public string Nom
        {
            get { return nom; }
            set
            {
                nom = value;
                OnPropertyChanged("Nom");
            }
        }

        [JsonProperty] 
        private string groupe;

        public string Groupe
        {
            get { return groupe; }
            set
            {
                groupe = value;
                OnPropertyChanged("Groupe");
            }
        }
    }
}