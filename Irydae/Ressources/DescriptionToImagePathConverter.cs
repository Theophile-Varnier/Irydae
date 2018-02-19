using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using Irydae.Helpers;

namespace Irydae.Ressources
{
    public class DescriptionToImagePathConverter : BaseConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imgName = "transparent.png";
            var enumValue = value as Enum;
            if (enumValue != null)
            {
                imgName = string.Format("{0}.png", enumValue.GetDescription());
            }
            return "/Images/" + imgName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}