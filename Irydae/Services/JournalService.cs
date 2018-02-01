using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            if(!Directory.Exists(Path.Combine(path, "Web")))
            {
                Directory.CreateDirectory(Path.Combine(path, "Web"));
            }
            DataPath = path;
        }

        public IEnumerable<string> GetExistingProfils()
        {
            return Directory.GetFiles(DataPath).Select(Path.GetFileNameWithoutExtension);
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
            List<Periode> res = new List<Periode>();

            string filePath = Path.Combine(DataPath, nomProfile + ".json");
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string periodes = sr.ReadToEnd();
                    res = JsonConvert.DeserializeObject<List<Periode>>(periodes);
                }
            }
            else
            {
                using (File.Create(filePath))
                { }
            }
            return res;
        }

        public void UpdateDatas(string nomPersonnage, IEnumerable<Periode> datas)
        {
            string filePath = Path.Combine(DataPath, nomPersonnage + ".json");
            File.WriteAllText(filePath, JsonConvert.SerializeObject(datas, Formatting.Indented));
        }
    }
}