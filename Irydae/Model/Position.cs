using Irydae.ViewModels;
using Newtonsoft.Json;

namespace Irydae.Model
{
    [JsonObject]
    public class Position : CatchedPropertyChanged
    {
        [JsonProperty] 
        private int x;

        [JsonIgnore]
        public int X
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged("X");
            }
        }

        [JsonProperty]
        private int y;

        [JsonIgnore]
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
                OnPropertyChanged("Y");
            }
        }
    }
}