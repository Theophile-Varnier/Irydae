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
            MainWindow mainWindow = new MainWindow(JournalService.Instance)
            {
                ViewModel = new MainWindowViewModel(JournalService.Instance)
            };

            Current.MainWindow = mainWindow;
            Current.MainWindow.Show();
        }
    }
}
