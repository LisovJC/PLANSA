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

        private int _index;

        public int Index
        {
            get => _index;
            set { _index = value; OnPropertyChanged(); }
        }




        public RelayCommand AddCheckBox { get; set; }
        public RelayCommand SaveItCommand { get; set; }
        public RelayCommand DeleteFile { get; set; }
        public ObservableCollection<FileItem> Files { get; set; }

        public EditPageViewModel()
        {
            checkBoxes = new ObservableCollection<CheckBoxItem>();
      
            TaskItems = new ObservableCollectionEX<TaskItem>();
            CurrentDatas = new ObservableCollectionEX<CurrentData>();
            TaskItems = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);
            CurrentDatas = DataSaveLoad.LoadData<CurrentData>(DataSaveLoad.JsonPathCurrentData);

            Files = new ObservableCollection<FileItem>();
            if (TaskItems.Count > 0)
            {
                for (int i = 0; i < TaskItems[CurrentDatas[0].SelectedPlan_1].files.Count; i++)
                {
                    Files.Add(new FileItem() { files = TaskItems[CurrentDatas[0].SelectedPlan_1].files[i] });
                }
            }

                PlanContent = TaskItems[CurrentDatas[0].numberPlanEdit].PlanContent;
            PlanLabel = TaskItems[CurrentDatas[0].numberPlanEdit].HeaderPlan;

            AddCheckBox = new RelayCommand(o =>
            {
                checkBoxes.Add(new CheckBoxItem() { MyProperty = true});
            });

            SaveItCommand = new RelayCommand(o =>
            {
                TaskItems[CurrentDatas[0].numberPlanEdit].PlanContent = PlanContent;
                TaskItems[CurrentDatas[0].numberPlanEdit].HeaderPlan = PlanLabel;
            });

            DeleteFile = new RelayCommand(o =>
            {
                DeleteFiles(Index);
            });
        }

        public void DeleteFiles(int selectedFile)
        {
            Files.RemoveAt(selectedFile);
            TaskItems[CurrentDatas[0].numberPlanEdit].files.RemoveAt(selectedFile);
        }
    }
}
