using System.Collections.ObjectModel;
using Irydae.ViewModels;
using Newtonsoft.Json;

namespace Irydae.Model
{
    [JsonObject]
    public class Rp : CatchedPropertyChanged
    {
        public Rp()
        {
            Partenaires = new ObservableCollection<Partenaire>();
        }
        [JsonProperty] 
        private string url;

        [JsonIgnore]
        public string Url
        {
            get { return url; }
            set
            {
                url = value;
                OnPropertyChanged("Url");
            }
        }

        [JsonProperty] 
        private string titre;

        [JsonIgnore]
        public string Titre
        {
            get
            {
                return titre;
            }
            set
            {
                titre = value;
                OnPropertyChanged("Titre");
            }
        }

        [JsonProperty("partenaires")]
        public ObservableCollection<Partenaire> Partenaires { get; set; }
    }
}