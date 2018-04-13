using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Irydae.Model;
using Irydae.ViewModels;

namespace Irydae.Views
{
    /// <summary>
    /// Logique d'interaction pour RelationsUserControl.xaml
    /// </summary>
    public partial class RelationsUserControl : UserControl
    {
        public MainWindowViewModel ViewModel
        {
            get { return DataContext as MainWindowViewModel; }
            set { DataContext = value; }
        }

        public RelationsUserControl()
        {
            InitializeComponent();
        }

        private bool dragging;

        private Point start, origin;

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
                Point position = e.GetPosition(GlobalCanvas);
                Image vava = sender as Image;
                if (vava != null)
                {
                    Partenaire partenaire = vava.DataContext as Partenaire;
                    if (partenaire != null)
                    {
                        partenaire.Position.X = (int)(position.X - 30);
                        partenaire.Position.Y = (int)(position.Y - 60);
                    }
                }
            }
        }
    }
}
