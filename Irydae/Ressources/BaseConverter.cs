using System;
using System.Windows.Markup;

namespace Irydae.Ressources
{
    public abstract class BaseConverter : MarkupExtension
    {
        /// <summary>
        /// Valeur d'initialisation de la propriété CheckTargetType lors de la création d'une instance d'un converter.
        /// </summary>
        /// <value>
        /// <c>true</c> if [check target type default value]; otherwise, <c>false</c>.
        /// </value>
        public static bool CheckTargetTypeInitValue { get; set; }

        /// <summary>
        /// Indique si le converter doit contrôler ou non le type cible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [check target type]; otherwise, <c>false</c>.
        /// </value>
        public bool CheckTargetType { get; set; }

        /// <summary>
        /// Initializes the <see cref="BaseConverter"/> class.
        /// </summary>
        static BaseConverter()
        {
            CheckTargetTypeInitValue = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseConverter"/> class.
        /// </summary>
        protected BaseConverter()
        {
            CheckTargetType = CheckTargetTypeInitValue;
        }

        #region MarkupExtension

        /// <summary>
        ///     En cas d'implémentation dans une classe dérivée, retourne un objet qui est défini comme valeur de la propriété
        ///     cible pour cette extension de balisage.
        /// </summary>
        /// <param name="serviceProvider">Objet qui peut fournir des services pour l'extension du balisage.</param>
        /// <returns>
        ///     Valeur d'objet à définir sur la propriété où l'extension est appliquée.
        /// </returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        #endregion
    }

}