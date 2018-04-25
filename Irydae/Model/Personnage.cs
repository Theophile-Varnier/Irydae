using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Irydae.Model
{
    [JsonObject]
    public class Personnage
    {
        public Personnage()
        {
            Periodes = new ObservableCollection<Periode>();
            Relations = new ObservableCollection<Partenaire>();
        }

        [JsonProperty]
        public ObservableCollection<Periode> Periodes { get; set; }

        [JsonProperty]
        public ObservableCollection<Partenaire> Relations { get; set; }
    }
}