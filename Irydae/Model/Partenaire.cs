using System.Runtime.Serialization;
using Irydae.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
        private Groupe groupe;

        [JsonIgnore]
        public Groupe Groupe
        {
            get { return groupe; }
            set
            {
                groupe = value;
                OnPropertyChanged("GroupeEnum");
            }
        }

        [OnError]
        public void OnError(StreamingContext context, ErrorContext errorContext)
        {
            errorContext.Handled = true;
        }
    }
}