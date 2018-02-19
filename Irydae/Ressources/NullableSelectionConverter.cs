using System;
using System.Windows;
using System.Windows.Data;
using Irydae.Model;

namespace Irydae.Ressources
{
    public class NullableSelectionConverter : BaseConverter, IValueConverter
    {
        public string NoneString { get; set; }

        public NullableSelectionConverter()
        {
            NoneString = string.Empty;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            object retval = value as RpType?;
            if (retval == null)
            {
                retval = NoneString;
            }
            return retval;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            object retval = null;
            if (value is RpType)
            {
                retval = value;
            }
            else if (!string.Equals(NoneString, value as string, StringComparison.OrdinalIgnoreCase))
            {
                retval = DependencyProperty.UnsetValue;
            }
            return retval;
        } 
    }
}