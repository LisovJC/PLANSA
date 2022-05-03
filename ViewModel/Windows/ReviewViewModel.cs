using PLANSA.Services;
using PLANSA.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PLANSA.ViewModel.Windows
{
    internal class ReviewViewModel : Observer
    {
        public RelayCommand CloseWindow { get; set; }

        private Page _choicePage = new MainPage();
        public Page ChoicePage
        {
            get => _choicePage;
            set => Set(ref _choicePage, value);
        }
        public ReviewViewModel()
        {
            ChoicePage = new MainPage();
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
