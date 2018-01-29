using System.Windows;
using Irydae.Services;
using Irydae.ViewModels;

namespace Irydae
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel
        {
            get { return DataContext as MainWindowViewModel; }
            set { DataContext = value; }
        }

        private readonly JournalService journalService;

        public MainWindow(JournalService service)
        {
            InitializeComponent();
            journalService = service;
        }
    }
}
