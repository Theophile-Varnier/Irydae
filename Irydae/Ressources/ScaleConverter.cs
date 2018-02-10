using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Irydae.Ressources
{
    class ScaleConverter : BaseConverter, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values[0] == DependencyProperty.UnsetValue)
            {
                return 0d;
            }
            var actualValue = (int)values[0];
            var pan = (double)values[1];
            var zoom = (double)values[2];

            var res = actualValue * zoom + pan;
            return res;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
