using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Irydae.Services;
using Irydae.ViewModels;
using Irydae.Views;

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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowAddRpDialog(object sender, RoutedEventArgs e)
        {
            AddRpDialog dialog = new AddRpDialog();
            dialog.Owner = this;
            dialog.DataContext = ViewModel.PersonnageInfo;
            dialog.ShowDialog();
        }

        private void ShowAddPeriodeDialog(object sender, RoutedEventArgs e)
        {
            AddPeriodeDialog dialog = new AddPeriodeDialog();
            dialog.Owner = this;
            dialog.DataContext = ViewModel.PersonnageInfo;
            dialog.ShowDialog();
        }

        private void ShowAddPartenaireDialog(object sender, RoutedEventArgs e)
        {
            AddPartenaireDialog dialog = new AddPartenaireDialog();
            dialog.Owner = this;
            dialog.DataContext = ViewModel.PersonnageInfo;
            dialog.ShowDialog();
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.S)
            {
                ViewModel.SaveDatas();
            }
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (ModificationStatusService.Instance.Dirty)
            {
                if (MessageBox.Show("Si tu quittes cette application sans avoir sauvegardé ces épuisantes modifications (Ctrl + S), le monde risque de s'effondrer et les anomalies régneront sans partage sur Irydaë. Et aussi va falloir recommencer.\n Tu es sûr que c'est ce que tu veux ?", "Attention malheureux !", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
