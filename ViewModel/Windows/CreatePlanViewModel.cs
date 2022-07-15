using PLANSA.Command;
using PLANSA.Model;
using PLANSA.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace PLANSA.ViewModel.Windows
{
    internal class CreatePlanViewModel : ObserverableObject
    {

        private bool WindowStateFlag { get; set; } = true;      

        #region Commands
        public RelayCommand CloseWindow { get; set; }
        public RelayCommand Maximize { get; set; }
        public RelayCommand Minimize { get; set; }
        public RelayCommand AddFile { get; set; }
        public RelayCommand ClearCommand { get; set; }
        public RelayCommand CreatePlan { get; set; }
        #endregion

        private string _visibleIcon;

        public string VisibleIcon
        {
            get => _visibleIcon;
            set { _visibleIcon = value; OnPropertyChanged(); }
        }

        private string _headerPlan;

        public string HeaderPlan
        {
            get => _headerPlan;
            set { _headerPlan = value; OnPropertyChanged(); }
        }

        private string _planContent;

        public string PlanContent
        {
            get => _planContent;
            set { _planContent = value; OnPropertyChanged(); }
        }

        private DateTime _dateForComplete;

        public DateTime DateForComplete
        {
            get => _dateForComplete;
            set { _dateForComplete = value; DateText = DateForComplete.ToString(); OnPropertyChanged(); }
        }

        private string _dateText;

        public string DateText
        {
            get => _dateText;
            set { _dateText = value; OnPropertyChanged(); }
        }


        public ObservableCollectionEX<TaskItem> TaskItems { get; set; }
        public ObservableCollection<FileItem> Files { get; set; }

        public CreatePlanViewModel()
        {
            Files = new ObservableCollection<FileItem>();
            TaskItems = new ObservableCollectionEX<TaskItem>();
            TaskItems = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);

            Maximize = new RelayCommand(o =>
            {
                if (WindowStateFlag)
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.Title == "CreatePlanWindow")
                        {
                            window.WindowState = WindowState.Maximized;
                        }                      
                    }
                    WindowStateFlag = false;
                }
                else
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.Title == "CreatePlanWindow")
                        {
                            window.WindowState = WindowState.Normal;
                        }
                    }
                    WindowStateFlag = true;
                }
            });

            Minimize = new RelayCommand(o =>
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.Title == "CreatePlanWindow")
                    {
                        window.WindowState = WindowState.Minimized;
                    }
                }
            });

            CloseWindow = new RelayCommand(o =>
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.Title == "CreatePlanWindow")
                    {
                        window.Close();
                    }
                    if (window.Title == "PLANSA")
                    {
                        window.Show();
                    }
                }
            });

            ClearCommand = new RelayCommand(o =>
            {
                PlanContent = String.Empty;
                HeaderPlan = String.Empty;
                DateText = String.Empty;
                Files.Clear();
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
                    VisibleIcon = "Hidden";
                }
            });

            CreatePlan = new RelayCommand(o =>
            {
                List<string> files = new List<string>();
                for (int i = 0; i < Files.Count; i++)
                {
                    files.Add(Files[i].files);
                }

                try
                {
                    TaskItems = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);
                }
                catch (Exception ex)
                {

                    Debug.WriteLine(ex.Message);
                }
                TaskItems.Add(new TaskItem() { files = files, HeaderPlan = HeaderPlan, DateAdd = DateTime.Now, PlanContent = PlanContent, DateComplete = DateForComplete, checkBoxes = new List<CheckBoxItem>() });
                DataSaveLoad.SaveDatas(DataSaveLoad.JsonPathTasks, TaskItems);


                foreach (Window window in Application.Current.Windows)
                {
                    if (window.Title == "CreatePlanWindow")
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
