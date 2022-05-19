using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PLANSA.ViewModel.Windows;
using System;
using System.IO;

namespace PLANSA.Services
{
    public class DataSaveLoad
    {
        public static string JsonPathTasks = $"{Environment.CurrentDirectory}\\Data\\UserData\\plansData.json";
        public static string JsonPathSettings = $"{Environment.CurrentDirectory}\\Setiings\\settingsData.json";
        public static string JsonPathCurrentData = $"{Environment.CurrentDirectory}\\Data\\CurrentData\\currentData.json";

        public static void Serialize(object o)
        {
            if(PlansaViewModel.TaskItems != null)
            {
                if (o.GetType() == PlansaViewModel.TaskItems.GetType())
                {
                    SaveDatas(JsonPathTasks, o);
                }
            }

            if (SettingsViewModel.settingsObj != null)
            {
                if (o.GetType() == SettingsViewModel.settingsObj.GetType())
                {
                    SaveDatas(JsonPathSettings, o);
                }
            }

            if (PlansaViewModel.CurrentDatas != null)
            {
                if (o.GetType() == PlansaViewModel.CurrentDatas.GetType())
                {
                    SaveDatas(JsonPathCurrentData, o);
                }
            }
        }

        private static void SaveDatas(string path, object o)
        {
            if (path != null)
            {
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    _ = Directory.CreateDirectory(dir);
                }

                using (StreamWriter file = File.CreateText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(file, o);
                }
            }
        }
     
        public static bool IsValidJson(string stringValue)
        {
            if (File.Exists(stringValue))
            {
                var value = File.ReadAllText(stringValue).Trim();
                if ((value.StartsWith("{") && value.EndsWith("}")) ||
                    (value.StartsWith("[") && value.EndsWith("]")))
                {
                    try
                    {
                        JToken obj = JToken.Parse(value);
                        return true;
                    }
                    catch (JsonReaderException)
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public static ObservableCollectionEX<T> LoadData<T>(string path)
        {
            if (!IsValidJson(path))
            {
                ObservableCollectionEX<T> objects = new ObservableCollectionEX<T>();
                return objects;
            }

            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<ObservableCollectionEX<T>>(json);
        }
    }
}
