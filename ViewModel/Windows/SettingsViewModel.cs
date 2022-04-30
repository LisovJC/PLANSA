using PLANSA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PLANSA.ViewModel.Windows
{
    internal class SettingsViewModel
    {
        public static string settingsPath = $"{Environment.CurrentDirectory}\\Setiings\\Settings.json";

        public RelayCommand CloseWindow { get; set; }
        public SettingsViewModel()
        {
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
    }
}
