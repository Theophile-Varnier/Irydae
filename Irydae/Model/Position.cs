using Irydae.ViewModels;
using Newtonsoft.Json;

namespace Irydae.Model
{
    [JsonObject]
    public class Position : AbstractPropertyChanged
    {
        [JsonProperty] 
        private int x;

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