using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Irydae.Ressources
{
    /// <summary>
    ///     Convert a boolean to a visibility
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : BaseConverter, IValueConverter
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BoolToVisibilityConverter" /> class.
        /// </summary>
        public BoolToVisibilityConverter()
        {
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }

        /// <summary>
        ///     Gets or sets the true value.
        /// </summary>
        /// <value>
        ///     The true value.
        /// </value>
        public Visibility TrueValue { get; set; }

        /// <summary>
        ///     Gets or sets the false value.
        /// </summary>
        /// <value>
        ///     The false value.
        /// </value>
        public Visibility FalseValue { get; set; }

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

            if ((bool)value)
            {
                return TrueValue;
            }
            return FalseValue;
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
        /// <exception cref="System.NotSupportedException">Convert back from a visibility to an integer is impossible</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Convert back from a visibility to an integer is impossible");
        }
    }
}