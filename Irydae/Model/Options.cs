﻿using System.Windows.Media;
using Irydae.ViewModels;

namespace Irydae.Model
{
    public class Options : AbstractPropertyChanged
    {
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
    }
}