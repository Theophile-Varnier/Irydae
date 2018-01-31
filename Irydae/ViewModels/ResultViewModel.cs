using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irydae.ViewModels
{
    class ResultViewModel : AbstractPropertyChanged
    {
        private string content;
        public string Content
        {
            get { return content; }
            set
            {
                content = value;
                OnPropertyChanged("Content");
            }
        }

        public ResultViewModel()
        {
            Uri = new Uri(string.Format("file:///{0}/result.html", Directory.GetCurrentDirectory()));
        }

        private Uri uri;

        public Uri Uri
        {
            get
            {
                return uri;
            }
            set
            {
                uri = value;
                OnPropertyChanged("Uri");
            }
        }
    }
}
