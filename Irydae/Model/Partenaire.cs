using Irydae.ViewModels;
using Newtonsoft.Json;

namespace Irydae.Model
{
    [JsonObject]
    public class Partenaire : CatchedPropertyChanged
    {
        [JsonProperty] 
        private string nom;

        [JsonIgnore]
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

        [JsonIgnore]
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