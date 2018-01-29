using System;
using System.Collections.Generic;
using System.IO;
using Irydae.Model;
using Newtonsoft.Json;

namespace Irydae.Services
{
    public class JournalService
    {
        private JournalService()
        {
            
        }

        public string DataPath { get; private set; }

        private static JournalService instance;

        private static void Initialize()
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
            instance.DataPath = path;
            string filePath = Path.Combine(instance.DataPath, "data.json");
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
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

        public IEnumerable<Periode> ParseDatas(string nomProfile)
        {
            List<Periode> res = null;

            string filePath = Path.Combine(DataPath, nomProfile + ".json");
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string periodes = sr.ReadToEnd();
                    res = JsonConvert.DeserializeObject<List<Periode>>(periodes);
                }
            }
            return res;
        }

        public void UpdateDatas(string nomPersonnage, IEnumerable<Periode> datas)
        {
            string filePath = Path.Combine(DataPath, nomPersonnage);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(datas, Formatting.Indented));
        }
    }
}