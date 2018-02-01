using Irydae.Views;
using System.Windows;

namespace Irydae.ViewModels
{
    class YesNoCancelDialogViewModel : AbstractPropertyChanged
    {
        private string title;

        public static MessageBoxResult ShowDialog(string message, string title, string yesMessage, string noMessage, string cancelMessage)
        {
            var viewModel = new YesNoCancelDialogViewModel
            {
                Title = title,
                YesMessage = yesMessage,
                ModalContent = message,
                NoMessage = noMessage,
                CancelMessage = cancelMessage
            };
            var dialog = new YesNoCancelDialog
            {
                DataContext = viewModel,
                Owner = Application.Current.MainWindow
            };
            dialog.ShowDialog();
            return dialog.Result;
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        private string yesMessage;

        public string YesMessage
        {
            get
            {
                return yesMessage;
            }
            set
            {
                yesMessage = value;
                OnPropertyChanged("YesMessage");
            }
        }

        private string noMessage;

        public string NoMessage
        {
            get
            {
                return noMessage;
            }
            set
            {
                noMessage = value;
                OnPropertyChanged("NoMessage");
            }
        }

        private string cancelMessage;

        public string CancelMessage
        {
            get
            {
                return cancelMessage;
            }
            set
            {
                cancelMessage = value;
                OnPropertyChanged("CancelMessage");
            }
        }

        private string modalContent;

        public string ModalContent
        {
            get
            {
                return modalContent;
            }
            set
            {
                modalContent = value;
                OnPropertyChanged("ModalContent");
            }
        }
    }
}
