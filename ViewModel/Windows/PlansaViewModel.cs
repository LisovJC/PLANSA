using Microsoft.Toolkit.Uwp.Notifications;
using PLANSA.Model;
using PLANSA.Services;
using PLANSA.View.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Reflection;

namespace PLANSA.ViewModel.Windows
{
    public class PlansaViewModel : Observer
    {
        #region Properties
        public static PlansaViewModel Instance { get; set; }

        private bool WindowStateFlag { get; set; } = true;

        private Visibility _visibility;

        public Visibility Visibility
        {
            get => _visibility;
            set { _visibility = value; OnPropertyChanged(); }
        }
        #endregion

        #region Flat_1

        private string _header;

        public string Header
        {
            get => _header;
            set { _header = value; OnPropertyChanged(); }
        }


        private int _index;

        public int SelectedIndex
        {
            get => _index;
            set { _index = value; OnPropertyChanged(); }
        }         
        
        private string _planContent;

        public string PlanContent
        {
            get => _planContent;
            set { _planContent = value; OnPropertyChanged(); }
        }

        
        private string _TimeOF;

        public string TimeOF
        {
            get => _TimeOF;
            set { _TimeOF = value; OnPropertyChanged(); }
        }

        
        private string _TimeOFDay;

        public string TimeOFDay
        {
            get => _TimeOFDay;
            set { _TimeOFDay = value; OnPropertyChanged(); }
        }

        
        private DateTime _dateComplete;

        public DateTime DeadLine
        {
            get => _dateComplete;
            set { _dateComplete = value; OnPropertyChanged(); }
        }

        
        private Brush _color;

        public Brush ColorPriority
        {
            get => _color;
            set { _color = value; OnPropertyChanged(); }
        }

        
        private Brush _colorNoty;

        public Brush ColorNoty
        {
            get => _colorNoty;
            set { _colorNoty = value; OnPropertyChanged(); }
        }

        private string _numbLab;

        public string NumberLabel
        {
            get => _numbLab;
            set { _numbLab = value; OnPropertyChanged(); }
        }        

        #endregion

        #region Flat_2

        private string _header_2;

        public string Header_2
        {
            get => _header_2;
            set { _header_2 = value; OnPropertyChanged(); }
        }

        private int _index_2;

        public int SelectedIndex_2
        {
            get => _index_2;
            set { _index_2 = value; OnPropertyChanged(); }
        }    

        private string _planContent_2;

        public string PlanContent_2
        {
            get => _planContent_2;
            set { _planContent_2 = value; OnPropertyChanged(); }
        }

        
        private string _TimeOF_2;

        public string TimeOF_2
        {
            get => _TimeOF_2;
            set { _TimeOF_2 = value; OnPropertyChanged(); }
        }
        
        
        private string _TimeOFDay_2;

        public string TimeOFDay_2
        {
            get => _TimeOFDay_2;
            set { _TimeOFDay_2 = value; OnPropertyChanged(); }
        }

       
        private DateTime _dateComplete_2;

        public DateTime DeadLine_2
        {
            get => _dateComplete_2;
            set { _dateComplete_2 = value; OnPropertyChanged(); }
        }

        
        private Brush _color_2;

        public Brush ColorPriority_2
        {
            get => _color_2;
            set { _color_2 = value; OnPropertyChanged(); }
        }

        
        private string _numbLab_2;

        public string NumberLabel_2
        {
            get => _numbLab_2;
            set { _numbLab_2 = value; OnPropertyChanged(); }
        }

        
        private Brush _colorNoty_2;

        public Brush ColorNoty_2
        {
            get => _colorNoty_2;
            set { _colorNoty_2 = value; OnPropertyChanged(); }
        }             
        #endregion

        #region Commands
        public RelayCommand CloseApp { get; set; }
        public RelayCommand Maximize { get; set; }
        public RelayCommand Minimize { get; set; }
        public RelayCommand OpenFile { get; set; }
        public RelayCommand LeftPlan { get; set; }
        public RelayCommand RightPlan { get; set; }
        public RelayCommand LeftPlan_2 { get; set; }
        public RelayCommand RightPlan_2 { get; set; }
        public RelayCommand OpenSettings { get; set; }
        public RelayCommand CreatePlanOpenWindow { get; set; }
        public RelayCommand ClipPlan_1 { get; set; }
        public RelayCommand ClipPlan_2 { get; set; }
        public RelayCommand ClearAll { get; set; }
        public RelayCommand AddFile { get; set; }
        public RelayCommand AddFile_2 { get; set; }
        public RelayCommand DeletePlan { get; set; }
        public RelayCommand DeletePlan_2 { get; set; }
        public RelayCommand NotyOnOff { get; set; }
        public RelayCommand NotyOnOff_2 { get; set; }
        public RelayCommand OpenReviewWindow { get; set; }
        public RelayCommand OpenEditPlan_1 { get; set; }
        public RelayCommand OpenEditPlan_2 { get; set; }
        #endregion

        #region Collections
        public ObservableCollection<FileItem> Files { get; set; }
        public static ObservableCollectionEX<TaskItem> TaskItems { get; set; }

        public ObservableCollection<FileItem> Files_2 { get; set; }
        public ObservableCollection<FileItem> Files_temp { get; set; }
        public ObservableCollection<TaskItem> Task_temp { get; set; }
        public static ObservableCollectionEX<Settings> settingsObj { get; set; }

        public static ObservableCollectionEX<CurrentData> CurrentDatas { get; set; }
        #endregion

        #region Clipping
        private Brush _clipColor_1;

        public Brush ClipColor_1
        {
            get => _clipColor_1;
            set { _clipColor_1 = value; OnPropertyChanged(); }
        }

       
        private Brush _clipColor_2;

        public Brush ClipColor_2
        {
            get => _clipColor_2;
            set { _clipColor_2 = value; OnPropertyChanged(); }
        }

        
        private bool _clip_1;

        public bool Clip_1
        {
            get => _clip_1;
            set { _clip_1 = value; OnPropertyChanged(); }
        }

        
        private bool _clip_2;

        public bool Clip_2
        {
            get => _clip_2;
            set { _clip_2 = value; OnPropertyChanged(); }
        }               
        #endregion

        public PlansaViewModel()
        {
            CurrentDatas = new ObservableCollectionEX<CurrentData>();
            CurrentDatas = DataSaveLoad.LoadData<CurrentData>(DataSaveLoad.JsonPathCurrentData);
            if (CurrentDatas.Count == 0)
                CurrentDatas.Add(new CurrentData() { Clipping_1 = true, Clipping_2 = true, Color_1 = "#0D7377", Color_2 = "#0D7377", numberPlanEdit = -1 });
            
            SettingsViewModel.CreateSettingsJSON();
            PropertyChanged += PlansaViewModel_PropertyChanged;
            Instance = this;

            StartApp();            
            Clips();

            #region Commands
            CloseApp = new RelayCommand(o =>
                {
                    Application.Current.Shutdown();
                });

            DeletePlan = new RelayCommand(o =>
            {
                try
                {
                    DeleteElement(CurrentDatas[0].SelectedPlan_1);                   
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e.Message);
                }
            });

            DeletePlan_2 = new RelayCommand(o =>
            {
                try
                {                   
                    DeleteElement(CurrentDatas[0].SelectedPlan_2);
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e.Message);
                }
            });

            ClearAll = new RelayCommand(o =>
                {
                    AllClear();

                });

            Maximize = new RelayCommand(o =>
                {
                    if (WindowStateFlag)
                    {
                        Application.Current.MainWindow.WindowState = WindowState.Maximized;
                        WindowStateFlag = false;
                    }
                    else
                    {
                        Application.Current.MainWindow.WindowState = WindowState.Normal;
                        WindowStateFlag = true;
                    }
                });

            Minimize = new RelayCommand(o =>
                {
                    var notify1 = new ToastContentBuilder();
                    notify1.AddText("PLANSA находится в трее!");
                    notify1.AddAppLogoOverride(new Uri($"{Environment.CurrentDirectory}\\PLANSA.ico"));
                    notify1.Show();
                    Application.Current.MainWindow.Hide();
                });

            NotyOnOff = new RelayCommand(o =>
            {
                colorNotyClick();
            });

            NotyOnOff_2 = new RelayCommand(o =>
            {
                colorNotyClick_2();
            });

            CreatePlanOpenWindow = new RelayCommand(o =>
                {                  
                    CreatePlanWindow window = new CreatePlanWindow();
                    window.Owner = Application.Current.MainWindow;
                    window.Show();
                    Application.Current.MainWindow.Hide();
                });

            OpenFile = new RelayCommand(o =>
                {
                    if (SelectedIndex == -1)
                    {
                        Debug.WriteLine("Индекс не выбран! Загружен первый элемент.");
                        SelectedIndex = 0;
                        Process.Start(Files[SelectedIndex].files);
                    }
                    else
                    {
                        Process.Start(Files[SelectedIndex].files);
                    }
                });

            RightPlan = new RelayCommand(o =>
                {
                    int valuePlan = CurrentDatas[0].SelectedPlan_1, limitRight = TaskItems.Count;
                    if (valuePlan < limitRight - 1)
                    {
                        valuePlan++;                        
                        CurrentDatas[0].SelectedPlan_1 = valuePlan;
                        TaskItems = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);
                        Files.Clear();
                        for (int i = 0; i < TaskItems[valuePlan].files.Count; i++)
                        {
                            Files.Add(new FileItem() { files = TaskItems[valuePlan].files[i] });
                        }
                        PlanContent = TaskItems[valuePlan].PlanContent;
                        DeadLine = TaskItems[valuePlan].DateComplete;
                        TimeOF = Math.Round((DeadLine - DateTime.Now).TotalHours, 1).ToString() + " Часов. ";
                        TimeOFDay = Math.Round((DeadLine - DateTime.Now).TotalDays, 1).ToString() + " Дней. ";
                        Header = TaskItems[valuePlan].HeaderPlan;
                        CalculateDeadLine();
                        colorNoty();
                        NumberLabel = $"{CurrentDatas[0].SelectedPlan_1 + 1} из {TaskItems.Count}";
                        TaskItems = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);
                    }
                });

            LeftPlan = new RelayCommand(o =>
                {
                    int valuePlan = /*int.Parse(File.ReadAllText(pathTonumberPlan))*/ CurrentDatas[0].SelectedPlan_1;
                    if (valuePlan > 0)
                    {
                        valuePlan--;
                        //File.WriteAllText(pathTonumberPlan, valuePlan.ToString());
                        CurrentDatas[0].SelectedPlan_1 = valuePlan;

                        TaskItems = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);
                        Files.Clear();
                        for (int i = 0; i < TaskItems[valuePlan].files.Count; i++)
                        {
                            Files.Add(new FileItem() { files = TaskItems[valuePlan].files[i] });
                        }
                        PlanContent = TaskItems[valuePlan].PlanContent;
                        DeadLine = TaskItems[valuePlan].DateComplete;
                        TimeOF = Math.Round((DeadLine - DateTime.Now).TotalHours, 1).ToString() + " Часов. ";
                        Header = TaskItems[valuePlan].HeaderPlan;
                        TimeOFDay = Math.Round((DeadLine - DateTime.Now).TotalDays, 1).ToString() + " Дней. ";
                        CalculateDeadLine();
                        colorNoty();
                        NumberLabel = $"{CurrentDatas[0].SelectedPlan_1 + 1} из {TaskItems.Count}";
                        TaskItems = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);
                    }
                });

            RightPlan_2 = new RelayCommand(o =>
                {
                    int valuePlan = /*int.Parse(File.ReadAllText(pathTonumberPlan_2))*/ CurrentDatas[0].SelectedPlan_2, limitRight = TaskItems.Count;
                    if (valuePlan < limitRight - 1)
                    {
                        valuePlan++;
                        //File.WriteAllText(pathTonumberPlan_2, valuePlan.ToString());
                        CurrentDatas[0].SelectedPlan_2 = valuePlan;

                        TaskItems = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);
                        Files_2.Clear();
                        for (int i = 0; i < TaskItems[valuePlan].files.Count; i++)
                        {
                            Files_2.Add(new FileItem() { files = TaskItems[valuePlan].files[i] });
                        }
                        PlanContent_2 = TaskItems[valuePlan].PlanContent;
                        DeadLine_2 = TaskItems[valuePlan].DateComplete;
                        TimeOF_2 = Math.Round((DeadLine_2 - DateTime.Now).TotalHours, 1).ToString() + " Часов. ";
                        TimeOFDay_2 = Math.Round((DeadLine_2 - DateTime.Now).TotalDays, 1).ToString() + " Дней. ";
                        CalculateDeadLine_2();
                        Header_2 = TaskItems[valuePlan].HeaderPlan;
                        NumberLabel_2 = $"{CurrentDatas[0].SelectedPlan_2 + 1} из {TaskItems.Count}";
                        colorNoty();
                        TaskItems = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);
                    }
                });

            LeftPlan_2 = new RelayCommand(o =>
                {
                    int valuePlan = /*int.Parse(File.ReadAllText(pathTonumberPlan_2))*/ CurrentDatas[0].SelectedPlan_2;
                    if (valuePlan > 0)
                    {
                        valuePlan--;
                        //File.WriteAllText(pathTonumberPlan_2, valuePlan.ToString());
                        CurrentDatas[0].SelectedPlan_2 = valuePlan;

                        TaskItems = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);
                        Files_2.Clear();
                        for (int i = 0; i < TaskItems[valuePlan].files.Count; i++)
                        {
                            Files_2.Add(new FileItem() { files = TaskItems[valuePlan].files[i] });
                        }
                        PlanContent_2 = TaskItems[valuePlan].PlanContent;
                        DeadLine_2 = TaskItems[valuePlan].DateComplete;
                        TimeOF_2 = Math.Round((DeadLine_2 - DateTime.Now).TotalHours, 1).ToString() + " Часов. ";
                        TimeOFDay_2 = Math.Round((DeadLine_2 - DateTime.Now).TotalDays, 1).ToString() + " Дней. ";
                        CalculateDeadLine_2();
                        Header_2 = TaskItems[valuePlan].HeaderPlan;
                        NumberLabel_2 = $"{CurrentDatas[0].SelectedPlan_2 + 1} из {TaskItems.Count}";
                        colorNoty();
                        TaskItems = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);

                    }
                });

            ClipPlan_1 = new RelayCommand(o =>
                {
                    if(Clip_1)
                    {
                        Clip_1 = false;
                        CurrentDatas[0].Clipping_1 = false;
                        ClipColor_1 = (Brush)new BrushConverter().ConvertFrom("#E23E57");
                        CurrentDatas[0].Color_1 = "#E23E57";
                    }
                    else
                    {
                        Clip_1 = true;
                        CurrentDatas[0].Clipping_1 = true;
                        ClipColor_1 = (Brush)new BrushConverter().ConvertFrom("#0D7377");
                        CurrentDatas[0].Color_1 = "#0D7377";
                    }
                });

            ClipPlan_2 = new RelayCommand(o =>
                {
                    if (Clip_2)
                    {
                        Clip_2 = false;
                        CurrentDatas[0].Clipping_2 = false;
                        ClipColor_2 = (Brush)new BrushConverter().ConvertFrom("#E23E57");
                        CurrentDatas[0].Color_2 = "#E23E57";
                    }
                    else
                    {
                        Clip_2 = true;
                        CurrentDatas[0].Clipping_2 = true;
                        ClipColor_2 = (Brush)new BrushConverter().ConvertFrom("#0D7377");
                        CurrentDatas[0].Color_2 = "#0D7377";
                    }
                });

            OpenSettings = new RelayCommand(o =>
             {
                 SettingsWindow window = new SettingsWindow();
                 window.Owner = Application.Current.MainWindow;
                 window.Show();
                 Application.Current.MainWindow.Hide();
             });

            OpenReviewWindow = new RelayCommand(o =>
            {
                ReviewWindow window = new ReviewWindow();
                window.Owner = Application.Current.MainWindow;
                window.Show();
                Application.Current.MainWindow.Hide();
            });

            OpenEditPlan_1 = new RelayCommand(o =>
            {
                CurrentDatas[0].numberPlanEdit = CurrentDatas[0].SelectedPlan_1;
                ReviewWindow window = new ReviewWindow();
                window.Owner = Application.Current.MainWindow;
                window.Show();
                Application.Current.MainWindow.Hide();
            });
            
            OpenEditPlan_2 = new RelayCommand(o =>
            {
                CurrentDatas[0].numberPlanEdit = CurrentDatas[0].SelectedPlan_2;
                ReviewWindow window = new ReviewWindow();
                window.Owner = Application.Current.MainWindow;
                window.Show();
                Application.Current.MainWindow.Hide();
            });

            AddFile = new RelayCommand(o =>
            {
                DefaultDialogService.OpenFileDialog();
                if (DefaultDialogService.FilePath == null)
                {

                }
                else
                {
                    if(Files.Count > 0)
                    {
                        Files_temp = new ObservableCollection<FileItem>();
                        Task_temp = new ObservableCollection<TaskItem>();
                        Files_temp = Files;
                        Files_temp.Add(new FileItem() { files = DefaultDialogService.FilePath });

                        List<string> tempList = new List<string>();
                        for (int i = 0; i < Files_temp.Count; i++)
                        {
                            tempList.Add(Files_temp[i].files);
                        }

                        int number = CurrentDatas[0].SelectedPlan_1;
                        Task_temp.Add(new TaskItem()
                        {
                            ColorPriority = TaskItems[number].ColorPriority,
                            DateAdd = TaskItems[number].DateAdd,
                            DateComplete = TaskItems[number].DateComplete,
                            PlanContent = TaskItems[number].PlanContent,
                            HeaderPlan = TaskItems[number].HeaderPlan,
                            Status = TaskItems[number].Status,
                            Failed = TaskItems[number].Failed,
                            files = tempList
                        });
                        TaskItems[number] = Task_temp[0];
                        LoadMainData();
                        Sorting();
                    }
                    else
                    {
                        Files_temp = new ObservableCollection<FileItem>();
                        Task_temp = new ObservableCollection<TaskItem>();                       
                        Files_temp.Add(new FileItem() { files = DefaultDialogService.FilePath });

                        List<string> tempList = new List<string>();
                        for (int i = 0; i < Files_temp.Count; i++)
                        {
                            tempList.Add(Files_temp[i].files);
                        }

                        int number = CurrentDatas[0].SelectedPlan_1;
                        Task_temp.Add(new TaskItem()
                        {
                            ColorPriority = TaskItems[number].ColorPriority,
                            DateAdd = TaskItems[number].DateAdd,
                            DateComplete = TaskItems[number].DateComplete,
                            PlanContent = TaskItems[number].PlanContent,
                            HeaderPlan = TaskItems[number].HeaderPlan,
                            Status = TaskItems[number].Status,
                            Failed = TaskItems[number].Failed,
                            files = tempList
                        });
                        TaskItems[number] = Task_temp[0];
                        LoadMainData();
                        Sorting();
                    }                    
                }
            });

            AddFile_2 = new RelayCommand(o =>
            {
                DefaultDialogService.OpenFileDialog();
                if (DefaultDialogService.FilePath == null)
                {

                }
                else
                {
                    if (Files_2.Count > 0)
                    {
                        Files_temp = new ObservableCollection<FileItem>();
                        Task_temp = new ObservableCollection<TaskItem>();
                        Files_temp = Files_2;
                        Files_temp.Add(new FileItem() { files = DefaultDialogService.FilePath });

                        List<string> tempList = new List<string>();
                        for (int i = 0; i < Files_temp.Count; i++)
                        {
                            tempList.Add(Files_temp[i].files);
                        }

                        int number = CurrentDatas[0].SelectedPlan_2;
                        Task_temp.Add(new TaskItem()
                        {
                            ColorPriority = TaskItems[number].ColorPriority,
                            DateAdd = TaskItems[number].DateAdd,
                            DateComplete = TaskItems[number].DateComplete,
                            PlanContent = TaskItems[number].PlanContent,
                            HeaderPlan = TaskItems[number].HeaderPlan,
                            Status = TaskItems[number].Status,
                            Failed = TaskItems[number].Failed,
                            files = tempList
                        });
                        TaskItems[number] = Task_temp[0];
                        LoadMainData();
                        Sorting();
                    }
                    else
                    {
                        Files_temp = new ObservableCollection<FileItem>();
                        Task_temp = new ObservableCollection<TaskItem>();
                        Files_temp.Add(new FileItem() { files = DefaultDialogService.FilePath });

                        List<string> tempList = new List<string>();
                        for (int i = 0; i < Files_temp.Count; i++)
                        {
                            tempList.Add(Files_temp[i].files);
                        }

                        int number = CurrentDatas[0].SelectedPlan_2;
                        Task_temp.Add(new TaskItem()
                        {
                            ColorPriority = TaskItems[number].ColorPriority,
                            DateAdd = TaskItems[number].DateAdd,
                            DateComplete = TaskItems[number].DateComplete,
                            PlanContent = TaskItems[number].PlanContent,
                            HeaderPlan = TaskItems[number].HeaderPlan,
                            Status = TaskItems[number].Status,
                            Failed = TaskItems[number].Failed,
                            files = tempList
                        });
                        TaskItems[number] = Task_temp[0];
                        LoadMainData();
                        Sorting();
                    }
                }
            });
            #endregion
        }

        #region Methods
        #region LoadOperaions
        void StartApp()
        {
            #region Flat_1
            settingsObj = new ObservableCollectionEX<Settings>();
            settingsObj = DataSaveLoad.LoadData<Settings>(DataSaveLoad.JsonPathSettings);
            SetAutoRun(settingsObj[0].AutoRun, Assembly.GetExecutingAssembly().Location);
                if (CurrentDatas[0].SelectedPlan_1 == -1)
                {
                    CurrentDatas[0].SelectedPlan_1 = 0;
                }

            Clip_1 = CurrentDatas[0].Clipping_1;
            Clip_2 = CurrentDatas[0].Clipping_2;
            Files = new ObservableCollection<FileItem>();
            TaskItems = new ObservableCollectionEX<TaskItem>();
            TaskItems = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);
            if (TaskItems.Count > 0)
            {
                for (int i = 0; i < TaskItems[CurrentDatas[0].SelectedPlan_1].files.Count; i++)
                {
                    Files.Add(new FileItem() { files = TaskItems[CurrentDatas[0].SelectedPlan_1].files[i] });
                }
                PlanContent = TaskItems[CurrentDatas[0].SelectedPlan_1].PlanContent;
                DeadLine = TaskItems[CurrentDatas[0].SelectedPlan_1].DateComplete;
                TimeOF = Math.Round((DeadLine - DateTime.Now).TotalHours, 1).ToString() + " Часов. ";
                TimeOFDay = Math.Round((DeadLine - DateTime.Now).TotalDays, 1).ToString() + " Дней. ";
                Header = TaskItems[CurrentDatas[0].SelectedPlan_1].HeaderPlan;
                NumberLabel = $"{CurrentDatas[0].SelectedPlan_1 + 1} из {TaskItems.Count}";
                CalculateDeadLine();
            }
            #endregion

            #region Flat_2
            Files_2 = new ObservableCollection<FileItem>();
            TaskItems = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);

            if (CurrentDatas[0].SelectedPlan_2 == -1)
            {
                CurrentDatas[0].SelectedPlan_2 = 0;
            }

            if (TaskItems.Count > 0)
            {
                for (int i = 0; i < TaskItems[CurrentDatas[0].SelectedPlan_2].files.Count; i++)
                {
                    Files_2.Add(new FileItem() { files = TaskItems[CurrentDatas[0].SelectedPlan_2].files[i] });
                }
                PlanContent_2 = TaskItems[CurrentDatas[0].SelectedPlan_2].PlanContent;
                DeadLine_2 = TaskItems[CurrentDatas[0].SelectedPlan_2].DateComplete;
                TimeOF_2 = Math.Round((DeadLine_2 - DateTime.Now).TotalHours, 1).ToString() + " Часов. ";
                TimeOFDay_2 = Math.Round((DeadLine - DateTime.Now).TotalDays, 1).ToString() + " Дней. ";
                Header_2 = TaskItems[CurrentDatas[0].SelectedPlan_2].HeaderPlan;
                NumberLabel_2 = $"{CurrentDatas[0].SelectedPlan_2 + 1} из {TaskItems.Count}";
                CalculateDeadLine_2();

                Sorting();
                PushNotyAsync();
                colorNoty();
                Clips();
            }
                #endregion

        }

        void Clips()
        {                       
                Clip_1 = CurrentDatas[0].Clipping_1;                      
                Clip_2 = CurrentDatas[0].Clipping_2;
                                
                ClipColor_1 = (Brush)new BrushConverter().ConvertFrom(CurrentDatas[0].Color_1);                                
                ClipColor_2 = (Brush)new BrushConverter().ConvertFrom(CurrentDatas[0].Color_2);           
        }

        public void LoadMainData()
        {
            CurrentDatas = DataSaveLoad.LoadData<CurrentData>(DataSaveLoad.JsonPathCurrentData);
            #region Flat_1
            if (CurrentDatas[0].SelectedPlan_1 == -1)
                {
                    CurrentDatas[0].SelectedPlan_1 = 0;
                }

            Files.Clear();
            TaskItems = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);
            if (TaskItems.Count > 0)
            {
                for (int i = 0; i < TaskItems[CurrentDatas[0].SelectedPlan_1].files.Count; i++)
                {
                    Files.Add(new FileItem() { files = TaskItems[CurrentDatas[0].SelectedPlan_1].files[i] });
                }
                PlanContent = TaskItems[CurrentDatas[0].SelectedPlan_1].PlanContent;
                DeadLine = TaskItems[CurrentDatas[0].SelectedPlan_1].DateComplete;
                TimeOF = Math.Round((DeadLine - DateTime.Now).TotalHours, 1).ToString() + " Часов. ";
                TimeOFDay = Math.Round((DeadLine - DateTime.Now).TotalDays, 1).ToString() + " Дней. ";
                Header = TaskItems[CurrentDatas[0].SelectedPlan_1].HeaderPlan;
                NumberLabel = $"{CurrentDatas[0].SelectedPlan_1 + 1} из {TaskItems.Count}";
                CalculateDeadLine();               
            }
            #endregion

            #region Flat_2
            if (CurrentDatas[0].SelectedPlan_2 == -1)
            {
                CurrentDatas[0].SelectedPlan_2 = 0;
            }

            Files_2.Clear();
            TaskItems = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);
            if (TaskItems.Count > 0)
            {
                for (int i = 0; i < TaskItems[CurrentDatas[0].SelectedPlan_2].files.Count; i++)
                {
                    Files_2.Add(new FileItem() { files = TaskItems[CurrentDatas[0].SelectedPlan_2].files[i] });
                }
                PlanContent_2 = TaskItems[CurrentDatas[0].SelectedPlan_2].PlanContent;
                DeadLine_2 = TaskItems[CurrentDatas[0].SelectedPlan_2].DateComplete;
                TimeOF_2 = Math.Round((DeadLine_2 - DateTime.Now).TotalHours, 1).ToString() + " Часов. ";
                TimeOFDay_2 = Math.Round((DeadLine_2 - DateTime.Now).TotalDays, 1).ToString() + " Дней. ";
                Header_2 = TaskItems[CurrentDatas[0].SelectedPlan_2].HeaderPlan;
                NumberLabel_2 = $"{CurrentDatas[0].SelectedPlan_2 + 1} из {TaskItems.Count}";
                CalculateDeadLine_2();
            }
            #endregion

            Clips();
            colorNoty();
        }

        private void PlansaViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(System.Windows.Visibility) && Visibility is System.Windows.Visibility.Visible)
            {
                LoadMainData();
                Sorting();
            }
        }
        #endregion

        #region DeadLinesOperations
        void CalculateDeadLine()
            {
                double value;
                value = Math.Round((DeadLine - DateTime.Now).TotalHours, 1);
                if (value > 264)
                {
                    ColorPriority = (Brush)new BrushConverter().ConvertFrom("#2EB872");
                }

                if (value < 264 && value > 72)
                {
                    ColorPriority = (Brush)new BrushConverter().ConvertFrom("#F7F48B");
                }

                if (value < 72 && value > 48)
                {
                    ColorPriority = (Brush)new BrushConverter().ConvertFrom("#F38181");
                }

                if (value < 48)
                {
                    ColorPriority = (Brush)new BrushConverter().ConvertFrom("#A40A3C");
                }
            }

        void CalculateDeadLine_2()
            {
                double value;
                value = Math.Round((DeadLine_2 - DateTime.Now).TotalHours, 1);
                if (value > 264)
                {
                    ColorPriority_2 = (Brush)new BrushConverter().ConvertFrom("#2EB872");
                }

                if (value < 264 && value > 72)
                {
                    ColorPriority_2 = (Brush)new BrushConverter().ConvertFrom("#F7F48B");
                }

                if (value < 72 && value > 48)
                {
                    ColorPriority_2 = (Brush)new BrushConverter().ConvertFrom("#F38181");
                }

                if (value < 48)
                {
                    ColorPriority_2 = (Brush)new BrushConverter().ConvertFrom("#A40A3C");
                }
            }

        void ClearDeadLine()
            {                            
                ColorPriority = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
                ColorPriority_2 = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
            }
        #endregion
        
        #region otherOperations
        void DeleteElement(int numberElement)
        {
            
            if(TaskItems.Count > 0)
            {
                TaskItems.RemoveAt(numberElement);

                if (TaskItems.Count == numberElement && (TaskItems.Count > 0))
                {                   
                    CurrentDatas[0].SelectedPlan_1--;
                }         
            }

            if (TaskItems.Count <= 0)
            {
                AllClear();
            }

            LoadMainData();
        }

        void DeleteElement_2(int numberElement)
        {

            if (TaskItems.Count > 0)
            {
                TaskItems.RemoveAt(numberElement);

                if (TaskItems.Count == numberElement && (TaskItems.Count > 0))
                {
                    CurrentDatas[0].SelectedPlan_2--;
                }
            }

            if (TaskItems.Count <= 0)
            {
                AllClear();
            }

            LoadMainData();
        }

        void AllClear()
        {
            Files.Clear();
            TaskItems.Clear();
            PlanContent_2 = String.Empty;
            DeadLine_2 = DateTime.MinValue;
            TimeOF_2 = String.Empty;
            PlanContent = String.Empty;
            DeadLine = DateTime.MinValue;
            TimeOF = String.Empty;
            Files_2.Clear();
            Header = String.Empty;
            Header_2 = String.Empty;
            NumberLabel = "0";
            NumberLabel_2 = "0";
            ClearDeadLine();
            CurrentDatas.Clear();            
        }

        void Sorting()
        {
            ObservableCollection<TaskItem> Temp = new ObservableCollection<TaskItem>(TaskItems.OrderBy(p => p.DateComplete));

            for (int i = 0; i < Temp.Count; i++)
            {
                TaskItems[i] = Temp[i];
            }

            Temp.Clear();
        }
#endregion

        #region Noty
        public void PushNoty()
        {
            while (true)
            {
                if (File.Exists(DataSaveLoad.JsonPathSettings))
                {
                    ObservableCollection<Settings> settings = new ObservableCollection<Settings>();
                    settings = DataSaveLoad.LoadData<Settings>(DataSaveLoad.JsonPathSettings);

                    for (int i = 0; i < TaskItems.Count; i++)
                    {
                        if (TaskItems[i].Noty)
                        {
                            if (settings[0].Hours1)
                            {
                                if ((TaskItems[i].DateComplete - DateTime.Now).TotalMinutes < 60)
                                {
                                    var notify1 = new ToastContentBuilder();
                                    notify1.AddText($"Посмотри, до выполнения задачи {TaskItems[i].HeaderPlan}, осталось менее часа!");
                                    notify1.AddAppLogoOverride(new Uri($"{Environment.CurrentDirectory}\\PLANSA.ico"));
                                    notify1.Show();
                                }
                            }

                            if (settings[0].Hours3)
                            {
                                if ((TaskItems[i].DateComplete - DateTime.Now).TotalMinutes < 180)
                                {
                                    var notify1 = new ToastContentBuilder();
                                    notify1.AddText($"Посмотри, до выполнения задачи {TaskItems[i].HeaderPlan}, осталось менее 3х часов!");
                                    notify1.AddAppLogoOverride(new Uri($"{Environment.CurrentDirectory}\\PLANSA.ico"));
                                    notify1.Show();
                                }
                            }

                            if (settings[0].Hours5)
                            {
                                if ((TaskItems[i].DateComplete - DateTime.Now).TotalMinutes < 350)
                                {
                                    var notify1 = new ToastContentBuilder();
                                    notify1.AddText($"Посмотри, до выполнения задачи {TaskItems[i].HeaderPlan}, осталось менее 5-ти часов!");
                                    notify1.AddAppLogoOverride(new Uri($"{Environment.CurrentDirectory}\\PLANSA.ico"));
                                    notify1.Show();
                                }
                            }
                        }
                    }
                    for (int i = 0; i < TaskItems.Count; i++)
                    {
                        if((TaskItems[i].DateComplete - DateTime.Now).TotalMinutes < 0)
                        {
                            var notify1 = new ToastContentBuilder();
                            notify1.AddText($"Наш план: {TaskItems[i].HeaderPlan}, провален! Срочно предпринимай действия!");
                            notify1.AddAppLogoOverride(new Uri($"{Environment.CurrentDirectory}\\PLANSA.ico"));
                            notify1.Show();
                            TaskItems[i].Failed = true;
                            TaskItems[i].Status = "Просрочено";
                        }
                    }
                }
                Thread.Sleep(40000);
            }
        }

        public async void PushNotyAsync()
        {
            await Task.Run(() => PushNoty());
        }

        void colorNotyClick()
        {
            if (!TaskItems[CurrentDatas[0].SelectedPlan_1].Noty)
            {
                TaskItems[CurrentDatas[0].SelectedPlan_1].Noty = true;
                ColorNoty = (Brush)new BrushConverter().ConvertFrom("#E23E57");
                LoadMainData();
            }
            else
            {
                TaskItems[CurrentDatas[0].SelectedPlan_1].Noty = false;
                ColorNoty = (Brush)new BrushConverter().ConvertFrom("#0D7377");
                LoadMainData();
            }
        }

        void colorNotyClick_2()
        {
            if (!TaskItems[CurrentDatas[0].SelectedPlan_2].Noty)
            {
                TaskItems[CurrentDatas[0].SelectedPlan_2].Noty = true;
                ColorNoty_2 = (Brush)new BrushConverter().ConvertFrom("#E23E57");
                LoadMainData();
            }
            else
            {
                TaskItems[CurrentDatas[0].SelectedPlan_2].Noty = false;
                ColorNoty_2 = (Brush)new BrushConverter().ConvertFrom("#0D7377");
                LoadMainData();
            }
        }

        void colorNoty()
        {
            try
            {
                if (!TaskItems[CurrentDatas[0].SelectedPlan_1].Noty)
                {
                    ColorNoty = (Brush)new BrushConverter().ConvertFrom("#0D7377");
                }
                else
                {
                    ColorNoty = (Brush)new BrushConverter().ConvertFrom("#E23E57");
                }

                if (!TaskItems[CurrentDatas[0].SelectedPlan_2].Noty)
                {
                    ColorNoty_2 = (Brush)new BrushConverter().ConvertFrom("#0D7377");
                }
                else
                {
                    ColorNoty_2 = (Brush)new BrushConverter().ConvertFrom("#E23E57");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
        }
        #endregion

        public static void SetAutoRun(bool autorun, string path)
        {
            const string name = "PLANSA";
            string ExePath = path;
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                if(autorun)
                {
                    reg.SetValue(name, ExePath);
                }
                else
                {
                    reg.DeleteValue(name);
                }

                reg.Flush();
                reg.Close();
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
