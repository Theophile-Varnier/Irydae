using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Irydae.Model;
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

        public MainWindow()
        {
            InitializeComponent();
        }


        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = !ViewModel.CheckModifications(false);
        }        

        private void MainWindow_OnContentRendered(object sender, EventArgs e)
        {
            ViewModel.Init();
        }

        private void ChangedTab(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabs = sender as TabControl;
            if (tabs != null)
            {
                TabItem item = tabs.SelectedItem as TabItem;
                if (item != null)
                {
                    string header = (string) item.Header;
                    if (header == "Rps")
                    {
                        ViewModel.DisplayMode = DisplayMode.Rps;
                    }
                    else if (header == "Relations")
                    {
                        ViewModel.DisplayMode = DisplayMode.Relations;
                    }
                }
            }
        }
    }
}
