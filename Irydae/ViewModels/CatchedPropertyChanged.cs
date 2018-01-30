using System.ComponentModel;
using Irydae.Services;

namespace Irydae.ViewModels
{
    public abstract class CatchedPropertyChanged : AbstractPropertyChanged
    {
        protected CatchedPropertyChanged()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ModificationStatusService.Instance.Dirty = true;
        }
    }
}