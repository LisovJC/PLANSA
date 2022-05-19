using PLANSA.Model;
using PLANSA.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLANSA.ViewModel.Pages
{
    internal class EditPageViewModel : Observer
    {
        public ObservableCollection<CheckBoxItem> checkBoxes { get; set; }
        public ObservableCollectionEX<TaskItem> TaskItems { get; set; }
        public ObservableCollectionEX<CurrentData> CurrentDatas { get; set; }

        private string _planContent;

        public string PlanContent
        {
            get => _planContent; 
            set { _planContent = value; OnPropertyChanged(); }
        }
        
        private string _planLabel;

        public string PlanLabel
        {
            get => _planLabel;
            set { _planLabel = value; OnPropertyChanged(); }
        }



        public RelayCommand AddCheckBox { get; set; }

        public EditPageViewModel()
        {
            checkBoxes = new ObservableCollection<CheckBoxItem>();

            TaskItems = new ObservableCollectionEX<TaskItem>();
            CurrentDatas = new ObservableCollectionEX<CurrentData>();
            TaskItems = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);
            CurrentDatas = DataSaveLoad.LoadData<CurrentData>(DataSaveLoad.JsonPathCurrentData);

            PlanContent = TaskItems[CurrentDatas[0].numberPlanEdit].PlanContent;
            PlanLabel = TaskItems[CurrentDatas[0].numberPlanEdit].HeaderPlan;

            AddCheckBox = new RelayCommand(o =>
            {
                checkBoxes.Add(new CheckBoxItem() { MyProperty = true});
            });
        }
    }
}
