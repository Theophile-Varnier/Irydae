using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using Irydae.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Irydae.Model
{
    [JsonObject]
    public class Partenaire : CatchedPropertyChanged
    {
        public Partenaire()
        {
            Position = new Position();
            RpsCommuns = new ObservableCollection<Rp>();
            LiensRpsCommuns = new List<string>();
        }
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
        private Groupe? groupe;

        [JsonIgnore]
        public Groupe? Groupe
        {
            get { return groupe; }
            set
            {
                groupe = value;
                OnPropertyChanged("Groupe");
            }
        }

        [OnError]
        public void OnError(StreamingContext context, ErrorContext errorContext)
        {
            errorContext.Handled = true;
        }

        [JsonProperty]
        private string description;

        [JsonIgnore]
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        [JsonProperty]
        private TypeRelation type;

        [JsonIgnore]
        public TypeRelation Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }

        [JsonProperty]
        private string avatarLink;

        [JsonIgnore]
        public string AvatarLink
        {
            get { return avatarLink; }
            set
            {
                avatarLink = value;
                OnPropertyChanged("AvatarLink");
            }
        }

        [JsonProperty]
        private Position position;

        [JsonIgnore]
        public Position Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }

        public void AjouterRpCommun(Rp rp)
        {
            if (RpsCommuns.All(r => r.Url != rp.Url))
            {
                RpsCommuns.Add(rp);
            }
            if (!LiensRpsCommuns.Contains(rp.Url))
            {
                LiensRpsCommuns.Add(rp.Url);
            }
        }

        [JsonIgnore]
        public ObservableCollection<Rp> RpsCommuns { get; set; }

        [JsonProperty]
        public List<string> LiensRpsCommuns { get; set; }
    }
}