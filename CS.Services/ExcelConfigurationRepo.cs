using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CS.Services.Interfaces;
using Newtonsoft.Json;
namespace CS.Services
{
    public class ExcelConfigurationRepo : IExcelConfigurationRepo
    {
        private readonly string directory = "Data";
        private readonly string excelConfigFile = "Excel.json";
        private string filePath;

        public ExcelConfigurationRepo(string contentRoot)
        {
            var dirPath = contentRoot + Path.DirectorySeparatorChar.ToString() + directory;
            Directory.CreateDirectory(dirPath);
            filePath = dirPath + Path.DirectorySeparatorChar.ToString() + excelConfigFile;
            if (!File.Exists(filePath))
            {
                File.CreateText(filePath).Close();
            }
        }

        public void Clear(int discriminator)
        {

            var configs = JsonConvert.DeserializeObject<Dictionary<int, List<ExcelConfig>>>(File.ReadAllText(filePath)) ?? new Dictionary<int, List<ExcelConfig>>();
            configs.Remove(discriminator);

            using (StreamWriter file = File.CreateText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, configs);
            }

        }

        public void ClearAll()
        {
            File.CreateText(filePath).Close();
        }

        public string[] Get(int discriminator, string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                return Array.Empty<string>();
            }

            var configs = JsonConvert.DeserializeObject<Dictionary<int, List<ExcelConfig>>>(File.ReadAllText(filePath)) ?? new Dictionary<int, List<ExcelConfig>>();

            return configs[discriminator]?.FirstOrDefault(x => x.Type.Contains(type))?.Columns ?? Array.Empty<string>();

        }

        public void Save(int discriminator, string type, string fileName, string[] columnNames)
        {

            var configs = JsonConvert.DeserializeObject<Dictionary<int, List<ExcelConfig>>>(File.ReadAllText(filePath)) ?? new Dictionary<int, List<ExcelConfig>>();


            List<ExcelConfig> configSet;
            if (!configs.TryGetValue(discriminator, out configSet))
            {
                configSet = new List<ExcelConfig>();
            }

            var excelConfig = new ExcelConfig()
            {
                Type = type,
                File = fileName,
                Columns = columnNames
            };

            if (!configSet.Any(x => x.Type == type))
            {
                configSet.Add(excelConfig);
            }
            else
            {
                var idx = configSet.FindIndex(x => x.Type == type);
                configSet[idx] = excelConfig;
            }

            configs[discriminator] = configSet;

            using (StreamWriter file = File.CreateText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, configs);
            }
        }
    }

    class ExcelConfig

    {
        public string Type
        {
            get;
            set;
        }

        public string File
        {
            get;
            set;
        }

        public string[] Columns
        {
            get;
            set;
        }
    }
}
