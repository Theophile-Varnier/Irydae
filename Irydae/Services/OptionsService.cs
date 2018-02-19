using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private Dictionary<string, Position> knownPositions;

        private static OptionsService instance;

        public static OptionsService Instance
        {
            get { return instance ?? (instance = new OptionsService()); }
        }

        public Options GetOptions(string profil)
        {
            var filePath = Path.Combine(JournalService.DataPath, "options", profil + ".json");

            if (!File.Exists(filePath))
            {
                filePath = Path.Combine(JournalService.DataPath, "options", "options.json");
                Options options = new Options();
                if (!File.Exists(filePath))
                {
                    SetDefaultValue(options);
                }
                else
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        var optionsString = sr.ReadToEnd();
                        options = JsonConvert.DeserializeObject<Options>(optionsString);
                        // Pas beau
                        if (options.CircleWidth == 0)
                        {
                            options.CircleWidth = 10;
                        }
                    }
                }
                SaveOptions(options, profil);
                return options;
            }
            else
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    var options = sr.ReadToEnd();
                    var res = JsonConvert.DeserializeObject<Options>(options);
                    // Pas beau
                    if (res.CircleWidth == 0)
                    {
                        res.CircleWidth = 10;
                    }
                    return res;
                }
            }
        }

        public void SetDefaultValue(Options options)
        {
            options.BorderColor = Color.FromRgb(0xFF, 0xFF, 0xFF);
            options.CircleColor = Color.FromRgb(0xFF, 0xA5, 0);
            options.DisplayByYear = false;
            options.LinkColor = Color.FromRgb(0, 0, 0);
            options.CircleWidth = 10;
            options.BorderRadius = 0;
            options.BorderRotation = 0;
        }

        public void SaveOptions(Options options, string profil)
        {
            var filePath = Path.Combine(JournalService.DataPath, "options", profil + ".json");
            File.WriteAllText(filePath, JsonConvert.SerializeObject(options, Formatting.Indented));
        }

        public Dictionary<string, Position> PredefinedPositions
        {
            get
            {
                if (knownPositions == null || !knownPositions.Any())
                {
                    var filePath = Path.Combine("Web", "dataMap.json");
                    if (File.Exists(filePath))
                    {
                        knownPositions = JsonConvert.DeserializeObject<Dictionary<string, Position>>(File.ReadAllText(filePath));
                    }
                }
                return knownPositions;
            }
        }
    }
}