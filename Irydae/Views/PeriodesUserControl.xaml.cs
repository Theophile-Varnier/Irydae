using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Irydae.ViewModels;

namespace Irydae.Views
{
    /// <summary>
    /// Logique d'interaction pour PeriodesUserControl.xaml
    /// </summary>
    public partial class PeriodesUserControl : UserControl
    {
        public MainWindowViewModel ViewModel
        {
            get { return DataContext as MainWindowViewModel; }
            set { DataContext = value; }
        }

        private bool dragging;

        private Point start, origin;

        public PeriodesUserControl()
        {
            InitializeComponent();
        }

        private void ShowAddRpDialog(object sender, RoutedEventArgs e)
        {
            AddRpDialog dialog = new AddRpDialog();
            dialog.Owner = Application.Current.MainWindow;
            dialog.DataContext = ViewModel.PersonnageInfo;
            dialog.ShowDialog();
        }

        private void ShowAddPeriodeDialog(object sender, RoutedEventArgs e)
        {
            AddPeriodeDialog dialog = new AddPeriodeDialog();
            dialog.Owner = Application.Current.MainWindow;
            dialog.DataContext = ViewModel.PersonnageInfo;
            dialog.ShowDialog();
        }

        private void ShowAddPartenaireDialog(object sender, RoutedEventArgs e)
        {
            AddPartenaireDialog dialog = new AddPartenaireDialog();
            dialog.DataContext = ViewModel.PersonnageInfo;
            dialog.ShowDialog();
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
                ViewModel.PersonnageInfo.SelectedPeriode.Position.X = (int)((position.X - 11 - ViewModel.CurrentPanX) / ViewModel.CurrentZoom);
                ViewModel.PersonnageInfo.SelectedPeriode.Position.Y = (int)((position.Y - 11 - ViewModel.CurrentPanY) / ViewModel.CurrentZoom);
            }
        }

        private void SetBounds(double x, double y)
        {
            var destX = ImageMap.ActualWidth * ViewModel.CurrentZoom;
            var destY = ImageMap.ActualHeight * ViewModel.CurrentZoom;
            var offsetX = Math.Min(0, Math.Max(x, CanvasMap.ActualWidth - destX));
            var offsetY = Math.Min(0, Math.Max(y, CanvasMap.ActualHeight - destY));
            var matTrans = ImageMap.RenderTransform as MatrixTransform;
            var mat = matTrans.Matrix;
            mat.OffsetX = offsetX;
            mat.OffsetY = offsetY;
            ViewModel.CurrentPanX = offsetX;
            ViewModel.CurrentPanY = offsetY;
            matTrans.Matrix = mat;
        }

        private void ImageMap_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var matTrans = ImageMap.RenderTransform as MatrixTransform;
            var pos1 = e.GetPosition(CanvasMap);

            var scale = e.Delta > 0 ? 1.1 : 1 / 1.1;

            var mat = matTrans.Matrix;
            mat.ScaleAt(scale, scale, pos1.X, pos1.Y);
            if (mat.M11 >= 1)
            {
                ViewModel.CurrentZoom = mat.M11;
                matTrans.Matrix = mat;
                SetBounds(mat.OffsetX, mat.OffsetY);
            }
        }

        private void ImageMap_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ImageMap.CaptureMouse();
            start = e.GetPosition(CanvasMap);
            var mat = ImageMap.RenderTransform as MatrixTransform;
            origin = new Point(mat.Matrix.OffsetX, mat.Matrix.OffsetY);
        }

        private void ImageMap_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (ImageMap.IsMouseCaptured)
            {
                Vector v = start - e.GetPosition(CanvasMap);
                SetBounds(origin.X - v.X, origin.Y - v.Y);
            }
        }

        private void ImageMap_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ImageMap.ReleaseMouseCapture();
        }

        private void PeriodesUserControl_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.S)
            {
                ViewModel.SaveDatas();
            }
            if (e.Key == Key.Delete)
            {
                ViewModel.PersonnageInfo.TryDelete(ViewModel.DisplayMode);
            }
            if (e.Key == Key.Escape)
            {
                var matTrans = ImageMap.RenderTransform as MatrixTransform;
                matTrans.Matrix = Matrix.Identity;
                ViewModel.CurrentZoom = 1;
                ViewModel.CurrentPanX = 0;
                ViewModel.CurrentPanY = 0;
            }
        }
    }
}
