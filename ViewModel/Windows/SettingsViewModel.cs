using PLANSA.Command;
using PLANSA.Model;
using PLANSA.Services;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows;

namespace PLANSA.ViewModel.Windows
{
    internal class SettingsViewModel : ObserverableObject
    {
        public static string settingsPath = $"{Environment.CurrentDirectory}\\Setiings\\Settings.json";

        private bool _autoRun;

        public bool autoRun
        {
            get => _autoRun; 
            set { _autoRun = value; OnPropertyChanged(); }
        }


        public static ObservableCollectionEX<Settings> settingsObj { get; set; }
        public RelayCommand CloseWindow { get; set; }
        public RelayCommand AutoRun { get; set; }
        public SettingsViewModel()
        {
            settingsObj = new ObservableCollectionEX<Settings>();
            settingsObj = DataSaveLoad.LoadData<Settings>(DataSaveLoad.JsonPathSettings);
            autoRun = settingsObj[0].AutoRun;
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

            AutoRun = new RelayCommand(o =>
            {
                if(settingsObj[0].AutoRun)
                {
                    autoRun = false;
                    settingsObj[0].AutoRun = autoRun;
                    DataSaveLoad.Serialize(settingsObj);
                    //PlansaViewModel.SetAutoRun(autoRun, Assembly.GetExecutingAssembly().Location);
                }
                else
                {
                    autoRun = true;
                    settingsObj[0].AutoRun = autoRun;
                    DataSaveLoad.Serialize(settingsObj);
                    //PlansaViewModel.SetAutoRun(autoRun, Assembly.GetExecutingAssembly().Location);
                }
            });
        }

        public static void CreateSettingsJSON()
        {
            if(!File.Exists(SettingsViewModel.settingsPath))
            {
                settingsObj = new ObservableCollectionEX<Settings>();
                settingsObj.Add(new Settings() {Hours1 = true, Hours3 = true, Hours5 = true, PushOn = true, AutoRun = true });
                //DataSaveLoad.SaveSettings(settingsObj);
            }
        }
    }
}
