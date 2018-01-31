using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Irydae.Views
{
    /// <summary>
    /// Logique d'interaction pour ResultDisplay.xaml
    /// </summary>
    public partial class ResultDisplay : Window
    {
        public ResultDisplay()
        {
            InitializeComponent();
            var uri = string.Format(@"file:///{0}{1}", AppDomain.CurrentDomain.BaseDirectory, System.IO.Path.Combine("Web","result.html"));
            Browser.Source = new Uri(uri);
        }
    }
}
