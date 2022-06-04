using PLANSA.Model;
using PLANSA.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PLANSA.ViewModel.Pages
{
    internal class EditPageViewModel : Observer
    {
        public static ObservableCollectionEX<CheckBoxItem> checkBoxes { get; set; }
        public ObservableCollectionEX<TaskItem> TaskItems { get; set; }
        public ObservableCollectionEX<CurrentData> CurrentDatas { get; set; }

        private int _indexCheck;

        public int SelectedIndex
        {
            get => _indexCheck; 
            set { _indexCheck = value; OnPropertyChanged(); }
        }


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

        private string _textProgress;

        public string textProgress
        {
            get => _textProgress; 
            set { _textProgress = value; OnPropertyChanged(); }
        }

        private double _progress;

        public double Progress
        {
            get => _progress;
            set { _progress = value; OnPropertyChanged(); }
        }

        public RelayCommand AddCheckBox { get; set; }
        public RelayCommand SaveItCommand { get; set; }
        public RelayCommand DeleteFile { get; set; }
        public RelayCommand AddFile { get; set; }
        public RelayCommand DeleteCheckBox { get; set; }
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

                Files = new ObservableCollection<FileItem>();
                if (TaskItems[CurrentDatas[0].numberPlanEdit].files.Count > 0)
                {
                    for (int i = 0; i < TaskItems[CurrentDatas[0].numberPlanEdit].files.Count; i++)
                    {
                        Files.Add(new FileItem() { files = TaskItems[CurrentDatas[0].numberPlanEdit].files[i] });
                    }
                }
                for (int i = 0; i < TaskItems[CurrentDatas[0].numberPlanEdit].checkBoxes.Count; i++)
                {
                    checkBoxes.Add(TaskItems[CurrentDatas[0].numberPlanEdit].checkBoxes[i]);
                }

                PlanContent = TaskItems[CurrentDatas[0].numberPlanEdit].PlanContent;
                PlanLabel = TaskItems[CurrentDatas[0].numberPlanEdit].HeaderPlan;
                DateAdd = TaskItems[CurrentDatas[0].numberPlanEdit].DateAdd;
                DateComplete = TaskItems[CurrentDatas[0].numberPlanEdit].DateComplete;
                CountFiles = Files.Count;
                CalculateProgress();
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            #endregion

            AddCheckBox = new RelayCommand(o =>
            {
                checkBoxes.Add(new CheckBoxItem());
            });

            DeleteCheckBox = new RelayCommand(o =>
            {
                try
                {
                    checkBoxes.RemoveAt(SelectedIndex);
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e.Message);
                }
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
                    
                        try
                        {
                            if (TaskItems[CurrentDatas[0].numberPlanEdit].checkBoxes.Count == 0)
                            {
                                TaskItems[CurrentDatas[0].numberPlanEdit].checkBoxes = new List<CheckBoxItem>();
                            }
                        }
                        catch (Exception e)
                        {

                            Debug.WriteLine(e.Message);
                        }
                        TaskItems[CurrentDatas[0].numberPlanEdit].checkBoxes.Clear();
                        for (int i = 0; i < checkBoxes.Count; i++)
                        {
                            TaskItems[CurrentDatas[0].numberPlanEdit].checkBoxes.Add(checkBoxes[i]);
                        }                    

                    DataSaveLoad.Serialize(TaskItems);                
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

        public void CalculateProgress()
        {
            double count = 0;
            double value = 0;
            for (int i = 0; i < checkBoxes.Count; i++)
            {
                if(checkBoxes[i].isCheck == true)
                {
                    count++;
                }
            }
            value = (count / checkBoxes.Count) * 100;
            Progress = value;
            textProgress = $"Прогресс выполнения({Progress}%)";
        }
    }
}
