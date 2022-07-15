using PLANSA.Command;
using PLANSA.Model;
using PLANSA.Services;
using PLANSA.View.Pages;
using System.Windows;
using System.Windows.Controls;

namespace PLANSA.ViewModel.Windows
{
    internal class ReviewViewModel : ObserverableObject
    {
        public RelayCommand CloseWindow { get; set; }
        public RelayCommand MainPage { get; set; }
        public RelayCommand MyPlansPage { get; set; }
        public RelayCommand EditPage { get; set; }
        public ObservableCollectionEX<CurrentData> CurrentDatas { get; set; }

        private Page _choicePage = new MainPage();
        public Page ChoicePage
        {
            get => _choicePage;
            set => Set(ref _choicePage, value);
        }
        public ReviewViewModel()
        {
            CurrentDatas = new ObservableCollectionEX<CurrentData>();
            CurrentDatas = DataSaveLoad.LoadData<CurrentData>(DataSaveLoad.JsonPathCurrentData);
            if((CurrentDatas[0].numberPlanEdit > -1))
            {
                ChoicePage = new EditPage();
            }
            else
            {
                ChoicePage = new MainPage();
            }
            CloseWindow = new RelayCommand(o =>
            {
                CurrentDatas[0].numberPlanEdit = -1;
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
