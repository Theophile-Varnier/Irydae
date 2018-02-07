using System;
using System.Globalization;
using System.Windows.Data;
using Irydae.Helpers;

namespace Irydae.Ressources
{
    public class AmbientValueConverter : BaseConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Enum myEnum = value as Enum;
            if (myEnum != null)
            {
                return myEnum.GetAmbientValue();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}