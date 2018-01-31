using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Irydae.Helpers
{
    public class RelayCommand : ICommand
    {
        #region Fields

        private readonly Predicate<object> canExecute;
        private readonly Action<object> execute;

        #endregion

        /// <summary>
        ///     Création d'une nouvelle commande.
        /// </summary>
        /// <param name="execute">L'action que la commande exécutera.</param>
        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }

        /// <summary>
        ///     Création d'une nouvelle commande.
        /// </summary>
        /// <param name="execute">L'action que la commande exécutera.</param>
        /// <param name="canExecute">Détermine si l'action peut s'exécuter.</param>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            this.execute = p => execute();
            if (canExecute != null)
            {
                this.canExecute = p => canExecute();
            }
        }

        /// <summary>
        ///     Création d'une nouvelle commande prenant des paramètres.
        /// </summary>
        /// <param name="execute">L'action que la commande exécutera.</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        ///     Création d'une nouvelle commande prenant des paramètres.
        /// </summary>
        /// <param name="execute">L'action que la commande exécutera.</param>
        /// <param name="canExecute">Détermine si l'action peut s'exécuter.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            this.execute = execute;
            this.canExecute = canExecute;
        }

        #region ICommand Members

        /// <summary>
        ///     Se produit lorsque des modifications influent sur l'exécution de la commande.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        ///     Définit la méthode qui détermine si la commande peut s'exécuter dans son état actuel.
        /// </summary>
        /// <param name="parameter">
        ///     Données utilisées par la commande.Si la commande ne requiert pas que les données soient
        ///     passées, cet objet peut avoir la valeur null.
        /// </param>
        /// <returns>
        ///     true si cette commande peut être exécutée ; sinon false.
        /// </returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        /// <summary>
        ///     Définit la méthode à appeler lorsque la commande est appelée.
        /// </summary>
        /// <param name="parameter">
        ///     Données utilisées par la commande.Si la commande ne requiert pas que les données soient
        ///     passées, cet objet peut avoir la valeur null.
        /// </param>
        public void Execute(object parameter)
        {
            execute(parameter);
        }

        #endregion
    }
}