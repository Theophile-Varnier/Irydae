using Irydae.ViewModels;
using System.Windows.Media;

namespace Irydae.Model
{
    public class Options : AbstractPropertyChanged
    {
        public static int OldMapWidth = 2269;
        public static int OldMapHeight = 2269;
        public static int NewMapWidthHeight = 650;
        public static int Crop = 115;

        private bool displayByYear;
        
        public bool DisplayByYear
        {
            get { return displayByYear; }
            set
            {
                displayByYear = value;
                OnPropertyChanged("DisplayByYear");
            }
        }

        private Color? circleColor;
        
        public Color? CircleColor
        {
            get
            {
                return circleColor;
            }
            set
            {
                circleColor = value;
                OnPropertyChanged("CircleColor");
            }
        }

        private Color? borderColor;

        public Color? BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                OnPropertyChanged("BorderColor");
            }
        }

        private Color? linkColor;

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

        private int circleWidth;

        public int CircleWidth
        {
            get
            {
                return circleWidth;
            }
            set
            {
                circleWidth = value;
                OnPropertyChanged("CircleWidth");
            }
        }

        private int borderRadius;

        public int BorderRadius
        {
            get
            {
                return borderRadius;
            }
            set
            {
                borderRadius = value;
                OnPropertyChanged("BorderRadius");
            }
        }

        private int borderRotation;

        public int BorderRotation
        {
            get
            {
                return borderRotation;
            }
            set
            {
                borderRotation = value;
                OnPropertyChanged("BorderRotation");
            }
        }

        private bool hideUpdate;

        public bool HideUpdate
        {
            get
            {
                return hideUpdate;
            }
            set
            {
                hideUpdate = value;
                OnPropertyChanged("HideUpdate");
            }
        }
    }
}