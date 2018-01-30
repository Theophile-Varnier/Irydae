﻿using System;
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

        [JsonProperty("rps")]
        public ObservableCollection<Rp> Rps { get; set; }
    }
}