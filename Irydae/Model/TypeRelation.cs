using System.Windows.Media;
using Irydae.ViewModels;
using Newtonsoft.Json;

namespace Irydae.Model
{
    [JsonObject]
    public class TypeRelation : AbstractPropertyChanged
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
        private Color? linkColor;

        [JsonIgnore]
        public Color? LinkColor
        {
            get
            {
                return linkColor;
            }
            set
            {
                linkColor = value;
                OnPropertyChanged("LinkColor");
            }
        }
    }
}