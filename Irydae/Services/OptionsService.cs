using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private void GestionRetro(Options options)
        {
            if (options.CircleWidth == 0)
            {
                options.CircleWidth = 10;
            }
            if (!options.TypesRelation.Any())
            {
                options.TypesRelation = new ObservableCollection<TypeRelation>(GetDefaultTypesRelation());
            }
        }

        public IEnumerable<TypeRelation> GetDefaultTypesRelation()
        {
            return new List<TypeRelation>
            {
                new TypeRelation
                {
                    LinkColor = Color.FromRgb(255, 255, 255),
                    Nom = "Connaissance"
                },
                new TypeRelation
                {
                    LinkColor = Color.FromRgb(255, 150, 150),
                    Nom = "Famille"
                },
                new TypeRelation
                {
                    LinkColor = Color.FromRgb(175, 0, 0),
                    Nom = "Ennemi"
                },
                new TypeRelation
                {
                    LinkColor = Color.FromRgb(0, 125, 0),
                    Nom = "Ami"
                },
            };
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
                        GestionRetro(options);
                    }
                }
                options.CharacterName = profil;
                SaveOptions(options, profil);
                return options;
            }
            using (StreamReader sr = new StreamReader(filePath))
            {
                var options = sr.ReadToEnd();
                var res = JsonConvert.DeserializeObject<Options>(options);
                GestionRetro(res);
                res.CharacterName = profil;
                return res;
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
            options.TypesRelation.Clear();
            var typesRelation = GetDefaultTypesRelation();
            foreach (TypeRelation typeRelation in typesRelation)
            {
                options.TypesRelation.Add(typeRelation);
            }
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