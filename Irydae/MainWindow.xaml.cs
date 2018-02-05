using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        private bool dragging;
        private double initX, initY;

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
            if(e.Key == Key.Delete)
            {
                ViewModel.PersonnageInfo.TryDelete();
            }
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = !ViewModel.CheckModifications(false);
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Init();
        }

        private void Canvas_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UIElement ellipse = (UIElement) sender;
            dragging = true;
            Mouse.Capture(ellipse);
            initX = Canvas.GetLeft(ellipse);
            initY = Canvas.GetTop(ellipse);
        }

        private void UIElement_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            dragging = false;
        }

        private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point position = e.GetPosition(CanvasMap);
                ViewModel.PersonnageInfo.SelectedPeriode.Position.X = (int)position.X - 11;
                ViewModel.PersonnageInfo.SelectedPeriode.Position.Y = (int) position.Y - 11;
            }
        }
    }
}
