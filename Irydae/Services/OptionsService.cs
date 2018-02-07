using System.IO;
using System.Windows.Media;
using Irydae.Model;
using Newtonsoft.Json;

namespace Irydae.Services
{
    public class OptionsService
    {
        private OptionsService()
        {
            
        }

        private static OptionsService instance;

        public static OptionsService Instance
        {
            get { return instance ?? (instance = new OptionsService()); }
        }

        public Options GetOptions()
        {
            var filePath = Path.Combine(JournalService.DataPath, "options", "options.json");

            if (!File.Exists(filePath))
            {
                Options options = new Options();
                SetDefaultValue(options);
                SaveOptions(options);
            }

            using (StreamReader sr = new StreamReader(filePath))
            {
                var options = sr.ReadToEnd();
                return JsonConvert.DeserializeObject<Options>(options);
            }
        }

        public void SetDefaultValue(Options options)
        {
            options.BorderColor = Color.FromRgb(0xFF, 0xFF, 0xFF);
            options.CircleColor = Color.FromRgb(0xFF, 0xA5, 0);
            options.DisplayByYear = false;
            options.LinkColor = Color.FromRgb(0, 0, 0);
        }

        public void SaveOptions(Options options)
        {
            var filePath = Path.Combine(JournalService.DataPath, "options", "options.json");
            File.WriteAllText(filePath, JsonConvert.SerializeObject(options, Formatting.Indented));
        }
    }
}