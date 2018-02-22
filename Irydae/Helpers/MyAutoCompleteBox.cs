using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Irydae.Helpers
{
    public class MyAutoCompleteBox : AutoCompleteBox
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if(e.Key == Key.Enter)
            {
                RaiseEnterKeyDownEvent();
            }
        }
        public event Action<object> EnterKeyDown;
        private void RaiseEnterKeyDownEvent()
        {
            var handler = EnterKeyDown;
            if (handler != null) handler(this);
        }
    }
}
