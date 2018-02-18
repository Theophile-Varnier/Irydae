using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Irydae.Ressources
{
    /// <summary>
    ///     Convert a nullity of an object into a boolean.
    /// </summary>
    [ValueConversion(typeof(object), typeof(CornerRadius))]
    public class PercentWidthCornerRadiusConverter : BaseConverter, IMultiValueConverter
    {

        #region IValueConverter

        /// <summary>
        ///     Convertit une valeur.
        /// </summary>
        /// <param name="value">Valeur produite par la source de liaison.</param>
        /// <param name="targetType">Type de la propriété de cible de liaison.</param>
        /// <param name="parameter">Paramètre de convertisseur à utiliser.</param>
        /// <param name="culture">Culture à utiliser dans le convertisseur.</param>
        /// <returns>
        ///     Une valeur convertie.Si la méthode retourne null, la valeur Null valide est utilisée.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">The target must be a Visibility</exception>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue)
            {
                return 0;
            }

            int actualWidth = (int)values[0];
            int percent;
            if (parameter == null || !int.TryParse((string)parameter, out percent))
            {
                percent = (int)values[1];
            }
            else
            {

            }

            return new CornerRadius(percent * actualWidth / 100);
        }

        /// <summary>
        ///     Convertit une valeur.
        /// </summary>
        /// <param name="value">Valeur produite par la cible de liaison.</param>
        /// <param name="targetType">Type dans lequel convertir.</param>
        /// <param name="parameter">Paramètre de convertisseur à utiliser.</param>
        /// <param name="culture">Culture à utiliser dans le convertisseur.</param>
        /// <returns>
        ///     Une valeur convertie.Si la méthode retourne null, la valeur Null valide est utilisée.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Convert back from a visibility to an object is impossible</exception>
        public object[] ConvertBack(object values, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Convert back from a bool to an object is impossible");
        }
        #endregion
    }
}