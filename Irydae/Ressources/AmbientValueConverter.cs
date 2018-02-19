using System;
using System.Globalization;
using System.Windows.Data;
using Irydae.Helpers;

namespace Irydae.Ressources
{
    public class AmbientValueConverter : BaseConverter, IValueConverter
    {
        public string NullLabel { get; set; }

        public AmbientValueConverter()
        {
            NullLabel = string.Empty;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Enum myEnum = value as Enum;
            if (myEnum != null)
            {
                return myEnum.GetAmbientValue();
            }
            return NullLabel;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}