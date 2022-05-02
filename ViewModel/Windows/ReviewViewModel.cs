using PLANSA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PLANSA.ViewModel.Windows
{
    internal class ReviewViewModel : Observer
    {
        public RelayCommand CloseWindow { get; set; }
        public ReviewViewModel()
        {
            CloseWindow = new RelayCommand(o =>
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.Title == "ReviewWindow")
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
