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
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            JournalService.Initialize();
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel(JournalService.Instance);
            MainWindow mainWindow = new MainWindow
            {
                ViewModel = mainWindowViewModel
            };

            Current.MainWindow = mainWindow;
            Current.MainWindow.Show();
        }
    }
}
