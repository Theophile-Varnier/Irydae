using System;
using System.Collections.ObjectModel;
using Irydae.ViewModels;
using Newtonsoft.Json;

namespace Irydae.Model
{
    [JsonObject]
    public class Periode : AbstractPropertyChanged
    {
        [JsonProperty] 
        private string lieu;

        public string Lieu
        {
            get { return lieu; }
            set
            {
                lieu = value;
                OnPropertyChanged("Lieu");
            }
        }

        [JsonProperty] 
        private DateTime debut;

        public DateTime DateDebut
        {
            get
            {
                return debut;
            }
            set
            {
                debut = value;
                OnPropertyChanged("DateDebut");
            }
        }

        [JsonProperty] 
        private DateTime? fin;

        public DateTime? DateFin
        {
            get { return fin; }
            set
            {
                fin = value;
                OnPropertyChanged("DateFin");
            }
        }

        [JsonProperty("rps")]
        public ObservableCollection<Rp> Rps { get; set; }
    }
}