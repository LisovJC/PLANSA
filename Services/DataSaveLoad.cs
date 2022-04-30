using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PLANSA.Model;
using System;
using System.IO;

namespace PLANSA.Services
{
    public class DataSaveLoad
    {
        public static string JsonPath = $"{Environment.CurrentDirectory}\\Tasks.json";
        public static string JsonPathSettings = $"{Environment.CurrentDirectory}\\Setiings\\Settings.json";
        public static void Serialize(object o)
        {
            if (JsonPath != null)
            {
                string dir = Path.GetDirectoryName(JsonPath);
                if (!Directory.Exists(dir))
                {
                    _ = Directory.CreateDirectory(dir);
                }

                using (StreamWriter file = File.CreateText(JsonPath))
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

        public static ObservableCollectionEX<TaskItem> LoadJson()
        {
            if (IsValidJson(JsonPath))
            {
                ObservableCollectionEX<TaskItem> Items = new ObservableCollectionEX<TaskItem>();
                string json = File.ReadAllText(JsonPath);
                Items = JsonConvert.DeserializeObject<ObservableCollectionEX<TaskItem>>(json);
                return Items;
            }
            else
            {
                ObservableCollectionEX<TaskItem> Items = new ObservableCollectionEX<TaskItem>();
                return Items;
            }
        }

        #region Settings
        public static void SaveSettings(object o)
        {
            if (JsonPathSettings != null)
            {
                string dir = Path.GetDirectoryName(JsonPathSettings);
                if (!Directory.Exists(dir))
                {
                    _ = Directory.CreateDirectory(dir);
                }

                using (StreamWriter file = File.CreateText(JsonPathSettings))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(file, o);
                }
            }
        }

        public static ObservableCollectionEX<Settings> LoadSettings()
        {
            if (IsValidJson(JsonPathSettings))
            {
                ObservableCollectionEX<Settings> Items = new ObservableCollectionEX<Settings>();
                string json = File.ReadAllText(JsonPathSettings);
                Items = JsonConvert.DeserializeObject<ObservableCollectionEX<Settings>>(json);
                return Items;
            }
            else
            {
                ObservableCollectionEX<Settings> Items = new ObservableCollectionEX<Settings>();
                return Items;
            }
        }

        #endregion
    }
}
