using PLANSA.Model;
using PLANSA.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PLANSA.ViewModel.Pages
{
    internal class EditPageViewModel : Observer
    {
        public static ObservableCollectionEX<CheckBoxItem> checkBoxes { get; set; }
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

        private DateTime _dateAdd;

        public DateTime DateAdd
        {
            get => _dateAdd;
            set { _dateAdd = value; OnPropertyChanged(); }
        }

        private DateTime _dateComplete;

        public DateTime DateComplete
        {
            get => _dateComplete;
            set { _dateComplete = value; OnPropertyChanged(); }
        }

        private int _countFiles;

        public int CountFiles
        {
            get => _countFiles;
            set { _countFiles = value; OnPropertyChanged(); }
        }



        public RelayCommand AddCheckBox { get; set; }
        public RelayCommand SaveItCommand { get; set; }
        public RelayCommand DeleteFile { get; set; }
        public RelayCommand AddFile { get; set; }
        public ObservableCollection<FileItem> Files { get; set; }

        public EditPageViewModel()
        {
            #region LoadData
            try
            {
                checkBoxes = new ObservableCollectionEX<CheckBoxItem>();

                TaskItems = new ObservableCollectionEX<TaskItem>();
                CurrentDatas = new ObservableCollectionEX<CurrentData>();
                TaskItems = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);
                CurrentDatas = DataSaveLoad.LoadData<CurrentData>(DataSaveLoad.JsonPathCurrentData);
                checkBoxes = DataSaveLoad.LoadData<CheckBoxItem>(DataSaveLoad.JsonPathCheckBoxData);

                Files = new ObservableCollection<FileItem>();
                if (TaskItems[CurrentDatas[0].numberPlanEdit].files.Count > 0)
                {
                    for (int i = 0; i < TaskItems[CurrentDatas[0].numberPlanEdit].files.Count; i++)
                    {
                        Files.Add(new FileItem() { files = TaskItems[CurrentDatas[0].numberPlanEdit].files[i] });
                    }
                }

                PlanContent = TaskItems[CurrentDatas[0].numberPlanEdit].PlanContent;
                PlanLabel = TaskItems[CurrentDatas[0].numberPlanEdit].HeaderPlan;
                DateAdd = TaskItems[CurrentDatas[0].numberPlanEdit].DateAdd;
                DateComplete = TaskItems[CurrentDatas[0].numberPlanEdit].DateComplete;
                CountFiles = Files.Count;
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            #endregion

            AddCheckBox = new RelayCommand(o =>
            {
                checkBoxes.Add(new CheckBoxItem() { textCheckBox = "Измени меня"});
            });

            SaveItCommand = new RelayCommand(o =>
            {
                TaskItems[CurrentDatas[0].numberPlanEdit].files.Clear();
                TaskItems[CurrentDatas[0].numberPlanEdit].PlanContent = PlanContent;
                TaskItems[CurrentDatas[0].numberPlanEdit].HeaderPlan = PlanLabel;
                TaskItems[CurrentDatas[0].numberPlanEdit].DateAdd = DateAdd;
                TaskItems[CurrentDatas[0].numberPlanEdit].DateComplete = DateComplete;
                for (int i = 0; i < Files.Count; i++)
                {
                    TaskItems[CurrentDatas[0].numberPlanEdit].files.Add(Files[i].files);
                }

                if(TaskItems[CurrentDatas[0].numberPlanEdit].files.Count == 0)
                {
                    for (int i = 0; i < Files.Count; i++)
                    {
                        TaskItems[CurrentDatas[0].numberPlanEdit].files.Add(Files[i].files);
                    }
                }

                DataSaveLoad.Serialize(TaskItems);
                DataSaveLoad.Serialize(checkBoxes);
            });

            DeleteFile = new RelayCommand(o =>
            {
                DeleteFiles(Index);
            });

            AddFile = new RelayCommand(o =>
            {
                DefaultDialogService.OpenFileDialog();
                if (DefaultDialogService.FilePath == null)
                {

                }
                else
                {
                    Files.Add(new FileItem() { files = DefaultDialogService.FilePath });                    
                }
            });
        }

        public void DeleteFiles(int selectedFile)
        {
            Files.RemoveAt(selectedFile);
            TaskItems[CurrentDatas[0].numberPlanEdit].files.RemoveAt(selectedFile);
        }
    }
}
