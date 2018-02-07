using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Irydae.ViewModels;
using Newtonsoft.Json;

namespace Irydae.Model
{
    [JsonObject]
    public class Periode : CatchedPropertyChanged
    {

        public Periode()
        {
            Rps = new ObservableCollection<Rp>();
            SubPeriodes = new List<Periode>();
        }

        [JsonProperty] 
        private string lieu;

        [JsonIgnore]
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

        [JsonIgnore]
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

        [JsonIgnore]
        public List<Periode> SubPeriodes { get; set; }
        
        [JsonProperty] 
        private DateTime? fin;

        [JsonIgnore]
        public DateTime? DateFin
        {
            get { return fin; }
            set
            {
                fin = value;
                OnPropertyChanged("DateFin");
            }
        }

        [JsonProperty] 
        private Position position;

        [JsonIgnore]
        public Position Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }

        [JsonProperty("rps")]
        public ObservableCollection<Rp> Rps { get; set; }
    }
}