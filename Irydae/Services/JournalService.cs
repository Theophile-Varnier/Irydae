using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Irydae.Helpers;
using Irydae.Model;
using Newtonsoft.Json;

namespace Irydae.Services
{
    public class JournalService
    {
        private JournalService()
        {

        }

        public static string DataPath { get; private set; }

        private static JournalService instance;

        public static void Initialize()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Irydae");
            if (instance == null)
            {
                instance = new JournalService();
            }
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!Directory.Exists(Path.Combine(path, "Web")))
            {
                Directory.CreateDirectory(Path.Combine(path, "Web"));
            }
            if (!Directory.Exists(Path.Combine(path, "options")))
            {
                Directory.CreateDirectory(Path.Combine(path, "options"));
            }
            DataPath = path;
        }

        public IEnumerable<string> GetExistingProfils()
        {
            return Directory.GetFiles(DataPath).Select(Path.GetFileNameWithoutExtension).Distinct();
        }

        public static JournalService Instance
        {
            get
            {
                if (instance == null)
                {
                    Initialize();
                }
                return instance;
            }
        }

        public Personnage ParseDatas(string nomProfile)
        {
            Personnage res = new Personnage();

            string filePath = Path.Combine(DataPath, nomProfile + ".json");
            if (File.Exists(filePath))
            {
                string profile;
                using (StreamReader sr = new StreamReader(filePath))
                {
                    profile = sr.ReadToEnd();
                }

                try
                {
                    res = JsonConvert.DeserializeObject<Personnage>(profile);
                }
                catch
                {
                    List<Periode> periodes = JsonConvert.DeserializeObject<List<Periode>>(profile);
                    if (periodes != null)
                    {
                        res.Periodes = new ObservableCollection<Periode>(periodes);

                    }
                }
            }
            foreach (var periode in res.Periodes)
            {
                foreach (var rp in periode.Rps)
                {
                    foreach (var partenaire in rp.Partenaires)
                    {
                        var relation = res.Relations.GetOrAddNew(p => p.Nom == partenaire.Nom, () => partenaire);
                        relation.AjouterRpCommun(rp);
                    }
                }
            }
            UpdateDatas(nomProfile, res);
            return res;
        }

        public void UpdateDatas(string nomPersonnage, Personnage datas)
        {
            string filePath = Path.Combine(DataPath, nomPersonnage + ".json");
            File.WriteAllText(filePath, JsonConvert.SerializeObject(datas, Formatting.Indented));
        }
    }
}