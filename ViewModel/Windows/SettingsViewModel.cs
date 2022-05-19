using PLANSA.Model;
using PLANSA.Services;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace PLANSA.ViewModel.Windows
{
    internal class SettingsViewModel
    {
        public static string settingsPath = $"{Environment.CurrentDirectory}\\Setiings\\Settings.json";

        public static ObservableCollectionEX<Settings> settingsObj { get; set; }
        public RelayCommand CloseWindow { get; set; }
        public SettingsViewModel()
        {
            settingsObj = new ObservableCollectionEX<Settings>();
            CloseWindow = new RelayCommand(o =>
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.Title == "SettingsWindow")
                    {
                        window.Close();
                    }
                    if (window.Title == "PLANSA")
                    {
                        window.Show();
                    }
                }
            });
        }

        public static void CreateSettingsJSON()
        {
            if(!File.Exists(SettingsViewModel.settingsPath))
            {
                settingsObj = new ObservableCollectionEX<Settings>();
                settingsObj.Add(new Settings() {Hours1 = true, Hours3 = false, Hours5 = false, PushOn = true });
                //DataSaveLoad.SaveSettings(settingsObj);
            }
        }
    }
}
