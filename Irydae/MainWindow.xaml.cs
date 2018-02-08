using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
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

        private Point start, origin;

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
            if (e.Key == Key.Delete)
            {
                ViewModel.PersonnageInfo.TryDelete();
            }
            if (e.Key == Key.Escape)
            {
                ViewModel.CurrentZoom = 1;
                ViewModel.CurrentPanX = 0;
                ViewModel.CurrentPanY = 0;
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
            UIElement ellipse = (UIElement)sender;
            dragging = true;
            Mouse.Capture(ellipse);
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
                ViewModel.PersonnageInfo.SelectedPeriode.Position.Y = (int)position.Y - 11;
            }
        }

        private void ImageMap_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var st = (ScaleTransform)((TransformGroup)ImageMap.RenderTransform).Children.First(tr => tr is ScaleTransform);
            double zoom = e.Delta > 0 ? .2 : -.2;
            ViewModel.CurrentZoom = Math.Max(1, st.ScaleX + zoom);
        }

        private void ImageMap_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ImageMap.CaptureMouse();
            var tt = (TranslateTransform)((TransformGroup)ImageMap.RenderTransform)
                .Children.First(tr => tr is TranslateTransform);
            start = e.GetPosition(CanvasMap);
            origin = new Point(tt.X, tt.Y);
        }

        private void ImageMap_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (ImageMap.IsMouseCaptured)
            {
                var destX = ImageMap.ActualWidth*ViewModel.CurrentZoom;
                var destY = ImageMap.ActualHeight * ViewModel.CurrentZoom;
                Vector v = start - e.GetPosition(CanvasMap);
                ViewModel.CurrentPanX = Math.Min(0, Math.Max(origin.X - v.X, CanvasMap.ActualWidth - destX));
                ViewModel.CurrentPanY = Math.Min(0, Math.Max(origin.Y - v.Y, CanvasMap.ActualHeight - destY));
            }
        }

        private void ImageMap_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ImageMap.ReleaseMouseCapture();
        }
    }
}
