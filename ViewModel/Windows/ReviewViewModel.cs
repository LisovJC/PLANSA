using PLANSA.Services;
using PLANSA.View.Pages;
using System.Windows;
using System.Windows.Controls;

namespace PLANSA.ViewModel.Windows
{
    internal class ReviewViewModel : Observer
    {
        public RelayCommand CloseWindow { get; set; }
        public RelayCommand MainPage { get; set; }
        public RelayCommand MyPlansPage { get; set; }
        public RelayCommand EditPage { get; set; }

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

            MainPage = new RelayCommand(o =>
            {
                ChoicePage = new MainPage();
            });

            MyPlansPage = new RelayCommand(o =>
            {
                ChoicePage = new MyPlansPage();
            });

            EditPage = new RelayCommand(o =>
            {
                ChoicePage = new EditPage();
            });
        }
    }
}
