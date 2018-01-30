using System.Windows;
using Irydae.Services;
using Irydae.ViewModels;

namespace Irydae
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindowViewModel mainViewModel;
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            JournalService.Initialize();
            mainViewModel = new MainWindowViewModel(JournalService.Instance);
            MainWindow mainWindow = new MainWindow
            {
                ViewModel = mainViewModel
            };

            Current.MainWindow = mainWindow;
            Current.MainWindow.Show();
        }
    }
}
