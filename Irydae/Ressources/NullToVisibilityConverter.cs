using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Irydae.Ressources
{
    [ValueConversion(typeof(object), typeof(Visibility))]
    public class NullToVisibilityConverter : BaseConverter, IValueConverter
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NullToVisibilityConverter" /> class.
        /// </summary>
        public NullToVisibilityConverter()
        {
            NonNullValue = Visibility.Visible;
            NullValue = Visibility.Collapsed;
        }

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
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (CheckTargetType && targetType != typeof(Visibility))
                throw new InvalidOperationException("The target must be a Visibility");

            if (value == null)
                return NullValue;
            return NonNullValue;
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
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Convert back from a visibility to an object is impossible");
        }

        #endregion

        /// <summary>
        ///     Gets or sets the non null value.
        /// </summary>
        /// <value>
        ///     The non null value.
        /// </value>
        public Visibility NonNullValue { get; set; }

        /// <summary>
        ///     Gets or sets the null value.
        /// </summary>
        /// <value>
        ///     The null value.
        /// </value>
        public Visibility NullValue { get; set; }
    }
}