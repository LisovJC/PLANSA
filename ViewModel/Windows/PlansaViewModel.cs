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
using WK.Libraries.BootMeUpNS;
using PLANSA.Command;

namespace PLANSA.ViewModel.Windows
{
    public class PlansaViewModel : ObserverableObject
    {
        #region NecessaryProperties
        public static PlansaViewModel Instance { get; set; }

        private Visibility _visibility;

        public Visibility Visibility
        {
            get => _visibility;
            set { _visibility = value; OnPropertyChanged(); }
        }

        private Visibility _visibilityEditPoint;

        public Visibility VisibilityEditPoint
        {
            get => _visibilityEditPoint;
            set { _visibilityEditPoint = value; OnPropertyChanged(); }
        }

        private int _indexPlan;

        public int indexPlan
        {
            get => _indexPlan; 
            set { _indexPlan = value; OnPropertyChanged(); }
        }

        private int _MindexPlan;

        public int MindexPlan
        {
            get => _MindexPlan;
            set { _MindexPlan = value; OnPropertyChanged(); }
        }

        private string _numberLabel;

        public string NumberLabel
        {
            get => _numberLabel; 
            set { _numberLabel = value; OnPropertyChanged(); }
        }

        private int _selectedIndex;

        public int selectedIndex
        {
            get => _selectedIndex;
            set => Set(ref _selectedIndex, value);
        }
        
        private int _selectedIndexFiles;

        public int selectedIndexFiles
        {
            get => _selectedIndexFiles;
            set => Set(ref _selectedIndexFiles, value);
        }
        #endregion

        #region PlanInformationProperties
        private string _planHeader;

        public string planHeader
        {
            get => _planHeader;
            set => Set(ref _planHeader, value);
        }

        private DateTime _deadLine;

        public DateTime deadLine
        {
            get => _deadLine;
            set => Set(ref _deadLine, value);
        }

        private string _hoursRemained;

        public string hoursRemained
        {
            get => _hoursRemained;
            set => Set(ref _hoursRemained, value);
        }

        private string _daysRemained;

        public string daysRemained
        {
            get => _daysRemained;
            set => Set(ref _daysRemained, value);
        }

        private string _WIP;

        public string WIP
        {
            get => _WIP;
            set => Set(ref _WIP, value);
        }

        private int _progressBar;

        public int progressBar
        {
            get => _progressBar;
            set => Set(ref _progressBar, value);
        }

        private string _planContent;

        public string planContent
        {
            get => _planContent;
            set => Set(ref _planContent, value);
        }


        #endregion

        #region MorePlanInformationProperties
        private string _MplanHeader;

        public string MplanHeader
        {
            get => _MplanHeader;
            set => Set(ref _MplanHeader, value);
        }

        private DateTime _MdeadLine;

        public DateTime MdeadLine
        {
            get => _MdeadLine;
            set => Set(ref _MdeadLine, value);
        }

        private string _MhoursRemained;

        public string MhoursRemained
        {
            get => _MhoursRemained;
            set => Set(ref _MhoursRemained, value);
        }

        private string _MdaysRemained;

        public string MdaysRemained
        {
            get => _MdaysRemained;
            set => Set(ref _MdaysRemained, value);
        }

        private string _MWIP;

        public string MWIP
        {
            get => _MWIP;
            set => Set(ref _MWIP, value);
        }

        private int _MprogressBar;

        public int MprogressBar
        {
            get => _MprogressBar;
            set => Set(ref _MprogressBar, value);
        }

        private string _MplanContent;

        public string MplanContent
        {
            get => _MplanContent;
            set => Set(ref _MplanContent, value);
        }
        #endregion

        #region HotCommands
        public RelayCommand CloseAppCommand { get; set; }
        public RelayCommand MaximizeCommand { get; set; }
        public RelayCommand MinimizeCommand { get; set; }        

        #endregion

        #region Collections

        public static ObservableCollectionEX<TaskItem> Plans { get; set; }         
        public static ObservableCollectionEX<CurrentData> CurrentDatas { get; set; }
        public ObservableCollection<CheckBoxItem> checkBoxes { get; set; }
        public ObservableCollection<FileItem> Files { get; set; }
        public ObservableCollection<FileItem> Files_temp { get; set; }
        public ObservableCollection<TaskItem> Plans_temp { get; set; }
        #endregion

        #region OpenCommands
        public RelayCommand CreatePlanOpenWindowCommand { get; set; }
        #endregion

        #region Commands
        public RelayCommand ChoosePlanCommand { get; set; }
        public RelayCommand RightPlanCommand { get; set; }
        public RelayCommand LeftPlanCommand { get; set; }
        public RelayCommand CreateCheckBoxCommand { get; set; }
        public RelayCommand RemoveCheckBoxCommand { get; set; }
        public RelayCommand RemovePlanCommand { get; set; }
        public RelayCommand AddFileCommand { get; set; }
        public RelayCommand OpenFileCommand { get; set; }
        #endregion

        #region PriorityColors
        private readonly string normalColor = "#75a77d";
        private readonly string lessNormalColor = "#E0F9B5";
        private readonly string warningColor = "#F9ED69";
        private readonly string lessWarningColor = "#F08A5D";
        private readonly string criticalColor = "#D72323";

        private Brush _priority;

        public Brush Priority
        {
            get => _priority;
            set => Set(ref _priority, value);
        }

        private Brush _Mpriority;

        public Brush MPriority
        {
            get => _Mpriority;
            set => Set(ref _Mpriority, value);
        }

        #endregion

        public PlansaViewModel()
        {
            #region NecessaryActions
            SettingsViewModel.CreateSettingsJSON();
            PropertyChanged += PlansaViewModel_PropertyChanged;
            Instance = this;
            #endregion

            #region LoadOperations
            Starting();
            #endregion

            #region HotCommands
            CloseAppCommand = new RelayCommand(o =>
                {
                    SaveCheckBoxes();
                    DataSaveLoad.SaveDatas(DataSaveLoad.JsonPathTasks, Plans);
                    Application.Current.Shutdown();
                });

            MaximizeCommand = new RelayCommand(o =>
                {                   
                    if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                    {
                        Application.Current.MainWindow.WindowState = WindowState.Normal;                       
                    }
                    else
                    {
                        Application.Current.MainWindow.WindowState = WindowState.Maximized;
                    }
                });

            MinimizeCommand = new RelayCommand(o =>
                {                   
                    var notify1 = new ToastContentBuilder();
                    notify1.AddText("PLANSA находится в трее!");
                    notify1.AddAppLogoOverride(new Uri($"{Environment.CurrentDirectory}\\PLANSA.ico"));
                    notify1.Show();
                    Application.Current.MainWindow.Hide();
                });
            #endregion            

            #region OpenCommands
            CreatePlanOpenWindowCommand = new RelayCommand(o =>
            {                
                CreatePlanWindow window = new CreatePlanWindow();
                window.Owner = Application.Current.MainWindow;
                window.Show();
                Application.Current.MainWindow.Hide();
            });
            #endregion

            #region Commands
            ChoosePlanCommand = new RelayCommand(o =>
            {
                SaveCheckBoxes();
                CurrentDatas[0].SelectedPlan_2 = CurrentDatas[0].SelectedPlan_1;
                MindexPlan = CurrentDatas[0].SelectedPlan_2;
                indexPlan = CurrentDatas[0].SelectedPlan_1;
                ChoosePlanLoad(MindexPlan);
                addPrioiry(indexPlan, MindexPlan);
                checkBoxes.Clear();
                Files.Clear();
                for (int i = 0; i < Plans[MindexPlan].checkBoxes.Count; i++)
                {
                    checkBoxes.Add(Plans[MindexPlan].checkBoxes[i]);
                }
                for (int i = 0; i < Plans[MindexPlan].files.Count; i++)
                {
                    Files.Add(new FileItem() { files = Plans[MindexPlan].files[i], FileName = Path.GetFileNameWithoutExtension(Plans[MindexPlan].files[i]) });
                }
                Loading();
            });

            RightPlanCommand = new RelayCommand(o =>
            {               
                int valuePlan = CurrentDatas[0].SelectedPlan_1, limitRight = Plans.Count;
                if (valuePlan < limitRight - 1)
                {
                    valuePlan++;
                    CurrentDatas[0].SelectedPlan_1 = valuePlan;
                    indexPlan = valuePlan;
                    MindexPlan = CurrentDatas[0].SelectedPlan_2;
                    Plans = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);                    
                    planContent = Plans[valuePlan].PlanContent;
                    deadLine = Plans[valuePlan].DateComplete;
                    hoursRemained = Math.Round((deadLine - DateTime.Now).TotalHours, 1).ToString() + " Ч";
                    daysRemained = Math.Round((deadLine - DateTime.Now).TotalDays, 1).ToString() + " Д";
                    planHeader = Plans[valuePlan].HeaderPlan;
                    NumberLabel = $"{CurrentDatas[0].SelectedPlan_1 + 1} из {Plans.Count}";
                    addPrioiry(indexPlan, MindexPlan);

                    Loading();
                }
            });

            LeftPlanCommand = new RelayCommand(o =>
            {                
                int valuePlan = CurrentDatas[0].SelectedPlan_1;
                if (valuePlan > 0)
                {
                    valuePlan--;
                    CurrentDatas[0].SelectedPlan_1 = valuePlan;
                    indexPlan = valuePlan;
                    MindexPlan = CurrentDatas[0].SelectedPlan_2;
                    Plans = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);                    
                    planContent = Plans[valuePlan].PlanContent;
                    deadLine = Plans[valuePlan].DateComplete;
                    hoursRemained = Math.Round((deadLine - DateTime.Now).TotalHours, 1).ToString() + " Ч";
                    planHeader = Plans[valuePlan].HeaderPlan;
                    daysRemained = Math.Round((deadLine - DateTime.Now).TotalDays, 1).ToString() + " Д";                                    
                    NumberLabel = $"{CurrentDatas[0].SelectedPlan_1 + 1} из {Plans.Count}";
                    addPrioiry(indexPlan, MindexPlan);

                    Loading();
                }
            });

            CreateCheckBoxCommand = new RelayCommand(o =>
            {
                checkBoxes.Add(new CheckBoxItem());
                SaveCheckBoxes();
            });

            RemoveCheckBoxCommand = new RelayCommand(o =>
            {
                checkBoxes.RemoveAt(selectedIndex);
                SaveCheckBoxes();
            });

            RemovePlanCommand = new RelayCommand(o =>
            {
            Plans.RemoveAt(MindexPlan);
            });

            AddFileCommand = new RelayCommand(o =>
            {
                DefaultDialogService.OpenFileDialog();
                if (DefaultDialogService.FilePath == null)
                {

                }
                else
                {
                    Files.Add(new FileItem() { files = DefaultDialogService.FilePath, FileName = Path.GetFileNameWithoutExtension(DefaultDialogService.FilePath) });
                    Plans[CurrentDatas[0].SelectedPlan_2].files.Clear();
                    foreach (var item in Files)
                    {
                        Plans[CurrentDatas[0].SelectedPlan_2].files.Add(item.files);
                    }
                }           
            });

            OpenFileCommand = new RelayCommand(o =>
            {
                if (selectedIndexFiles == -1)
                {
                    Debug.WriteLine("Индекс не выбран! Загружен первый элемент.");
                    selectedIndexFiles = 0;
                    Process.Start(Files[selectedIndexFiles].files);
                }
                else
                {
                    Process.Start(Files[selectedIndexFiles].files);
                }
            });
            #endregion
        }

        #region Methods

        #region LoadOperaions        
        #region LoadWindow
        private void PlansaViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(System.Windows.Visibility) && Visibility is System.Windows.Visibility.Visible)
            {
                try
                {
                    Loading();
                }
                catch (Exception ex)
                {

                    Debug.WriteLine(ex.Message);
                }
            }
        }
        #endregion

        #region Loading
        public void Starting()
        {
            try
            {
                Plans = new ObservableCollectionEX<TaskItem>();
                Plans = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);               

                CurrentDatas = new ObservableCollectionEX<CurrentData>();
                CurrentDatas = DataSaveLoad.LoadData<CurrentData>(DataSaveLoad.JsonPathCurrentData);
                if (CurrentDatas.Count == 0)
                    CurrentDatas.Add(new CurrentData() { SelectedPlan_1 = 0, SelectedPlan_2 = 0 });

                checkBoxes = new ObservableCollection<CheckBoxItem>();
                if (Plans[CurrentDatas[0].SelectedPlan_2].checkBoxes.Count > 0)
                for (int i = 0; i < Plans[CurrentDatas[0].SelectedPlan_2].checkBoxes.Count; i++)
                {
                    checkBoxes.Add(Plans[CurrentDatas[0].SelectedPlan_2].checkBoxes[i]);
                }

                Files = new ObservableCollection<FileItem>();
                if (Plans[CurrentDatas[0].SelectedPlan_2].files.Count > 0)
                    for (int i = 0; i < Plans[CurrentDatas[0].SelectedPlan_2].files.Count; i++)
                    {
                        Files.Add(new FileItem() { files = Plans[CurrentDatas[0].SelectedPlan_2].files[i], FileName = Path.GetFileNameWithoutExtension(Plans[CurrentDatas[0].SelectedPlan_2].files[i]) });
                    }

                indexPlan = CurrentDatas[0].SelectedPlan_1;
                MindexPlan = CurrentDatas[0].SelectedPlan_2;

                planHeader = Plans[indexPlan].HeaderPlan;
                hoursRemained = Math.Round((Plans[indexPlan].DateComplete - DateTime.Now).TotalHours, 1).ToString() + " Ч";
                daysRemained = Math.Round((Plans[indexPlan].DateComplete - DateTime.Now).TotalDays, 1).ToString() + " Д";
                deadLine = Plans[indexPlan].DateComplete;
                planContent = Plans[indexPlan].PlanContent;
                NumberLabel = $"{CurrentDatas[0].SelectedPlan_1 + 1} из {Plans.Count}";
                
                MplanHeader = Plans[MindexPlan].HeaderPlan;
                MhoursRemained = Math.Round((Plans[MindexPlan].DateComplete - DateTime.Now).TotalHours, 1).ToString() + " Ч";
                MdaysRemained = Math.Round((Plans[MindexPlan].DateComplete - DateTime.Now).TotalDays, 1).ToString() + " Д";
                MdeadLine = Plans[MindexPlan].DateComplete;
                MplanContent = Plans[indexPlan].PlanContent;

                if(indexPlan == MindexPlan)
                {
                    VisibilityEditPoint = Visibility.Visible;
                }
                else
                {
                    VisibilityEditPoint = Visibility.Hidden;
                }

                addPrioiry(indexPlan, MindexPlan);
                Sorting();
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
            }            
        }

        public void Loading()
        {
            Plans = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);

            CurrentDatas = DataSaveLoad.LoadData<CurrentData>(DataSaveLoad.JsonPathCurrentData);
            if (CurrentDatas.Count == 0)
                CurrentDatas.Add(new CurrentData() { SelectedPlan_1 = 0, SelectedPlan_2 = 0 });
            
            if (Plans[CurrentDatas[0].SelectedPlan_2].checkBoxes.Count > 0)
                for (int i = 0; i < Plans[CurrentDatas[0].SelectedPlan_2].checkBoxes.Count; i++)
                {
                    checkBoxes.Add(Plans[CurrentDatas[0].SelectedPlan_2].checkBoxes[i]);
                }
           
            if (Plans[CurrentDatas[0].SelectedPlan_2].files.Count > 0)
                for (int i = 0; i < Plans[CurrentDatas[0].SelectedPlan_2].files.Count; i++)
                {
                    Files.Add(new FileItem() { files = Plans[CurrentDatas[0].SelectedPlan_2].files[i], FileName = Path.GetFileNameWithoutExtension(Plans[CurrentDatas[0].SelectedPlan_2].files[i]) });
                }

            indexPlan = CurrentDatas[0].SelectedPlan_1;
            MindexPlan = CurrentDatas[0].SelectedPlan_2;

            planHeader = Plans[indexPlan].HeaderPlan;
            hoursRemained = Math.Round((Plans[indexPlan].DateComplete - DateTime.Now).TotalHours, 1).ToString() + " Ч";
            daysRemained = Math.Round((Plans[indexPlan].DateComplete - DateTime.Now).TotalDays, 1).ToString() + " Д";
            deadLine = Plans[indexPlan].DateComplete;
            planContent = Plans[indexPlan].PlanContent;
            NumberLabel = $"{CurrentDatas[0].SelectedPlan_1 + 1} из {Plans.Count}";

            MplanHeader = Plans[MindexPlan].HeaderPlan;
            MhoursRemained = Math.Round((Plans[MindexPlan].DateComplete - DateTime.Now).TotalHours, 1).ToString() + " Ч";
            MdaysRemained = Math.Round((Plans[MindexPlan].DateComplete - DateTime.Now).TotalDays, 1).ToString() + " Д";
            MdeadLine = Plans[MindexPlan].DateComplete;
            MplanContent = Plans[indexPlan].PlanContent;

            if (indexPlan == MindexPlan)
            {
                VisibilityEditPoint = Visibility.Visible;
            }
            else
            {
                VisibilityEditPoint = Visibility.Hidden;
            }

            addPrioiry(indexPlan, MindexPlan);
            Sorting();
        }
        #endregion

        #region ChoosePlanLoad
        public void ChoosePlanLoad(int index)
        {
            MplanHeader = Plans[index].HeaderPlan;
            MhoursRemained = Math.Round((Plans[index].DateComplete - DateTime.Now).TotalHours, 1).ToString() + " Ч";
            MdaysRemained = Math.Round((Plans[index].DateComplete - DateTime.Now).TotalDays, 1).ToString() + " Д";
            MdeadLine = Plans[index].DateComplete;
            MplanContent = Plans[indexPlan].PlanContent;
        }
        #endregion
        #endregion

        #region Sorting
        void Sorting()
        {
            ObservableCollection<TaskItem> Temp = new ObservableCollection<TaskItem>(Plans.OrderBy(p => p.DateComplete));

            for (int i = 0; i < Temp.Count; i++)
            {
                Plans[i] = Temp[i];
            }

            Temp.Clear();
        }
        #endregion

        #region colorityPriority
        public void addPrioiry(int index1, int index2)
        {          
            if ((Plans[index1].DateComplete - DateTime.Now).TotalHours >= 96 )
                Priority = (Brush)new BrushConverter().ConvertFrom(normalColor);

            if (((Plans[index1].DateComplete - DateTime.Now).TotalHours < 96) && ((Plans[index1].DateComplete - DateTime.Now).TotalHours >= 72))
                Priority = (Brush)new BrushConverter().ConvertFrom(lessNormalColor);

            if (((Plans[index1].DateComplete - DateTime.Now).TotalHours < 72) && ((Plans[index1].DateComplete - DateTime.Now).TotalHours >= 48))
                Priority = (Brush)new BrushConverter().ConvertFrom(warningColor);

            if (((Plans[index1].DateComplete - DateTime.Now).TotalHours < 48) && ((Plans[index1].DateComplete - DateTime.Now).TotalHours >= 32))
                Priority = (Brush)new BrushConverter().ConvertFrom(lessWarningColor);

            if ((Plans[index1].DateComplete - DateTime.Now).TotalHours < 32)
                Priority = (Brush)new BrushConverter().ConvertFrom(criticalColor);

            if ((Plans[index2].DateComplete - DateTime.Now).TotalHours >= 96)
                MPriority = (Brush)new BrushConverter().ConvertFrom(normalColor);

            if (((Plans[index2].DateComplete - DateTime.Now).TotalHours < 96) && ((Plans[index2].DateComplete - DateTime.Now).TotalHours >= 72))
                MPriority = (Brush)new BrushConverter().ConvertFrom(lessNormalColor);

            if (((Plans[index2].DateComplete - DateTime.Now).TotalHours < 72) && ((Plans[index2].DateComplete - DateTime.Now).TotalHours >= 48))
                MPriority = (Brush)new BrushConverter().ConvertFrom(warningColor);

            if (((Plans[index2].DateComplete - DateTime.Now).TotalHours < 48) && ((Plans[index2].DateComplete - DateTime.Now).TotalHours >= 32))
                MPriority = (Brush)new BrushConverter().ConvertFrom(lessWarningColor);

            if ((Plans[index2].DateComplete - DateTime.Now).TotalHours < 32)
                MPriority = (Brush)new BrushConverter().ConvertFrom(criticalColor);
        }
        #endregion

        #region SavingCheckBoxes
        public void SaveCheckBoxes()
        {
            if (checkBoxes.Count > 0)
            {
                try
                {
                    if (Plans[MindexPlan].checkBoxes.Count == 0)
                    {
                        Plans[MindexPlan].checkBoxes = new List<CheckBoxItem>();
                    }
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e.Message);
                }
                Plans[MindexPlan].checkBoxes.Clear();
                for (int i = 0; i < checkBoxes.Count; i++)
                {
                    Plans[MindexPlan].checkBoxes.Add(checkBoxes[i]);
                }
            }

            DataSaveLoad.Serialize(Plans);
        }
        #endregion

        #endregion













        //#region Noty
        //public void PushNoty()
        //{
        //    while (true)
        //    {
        //        if (File.Exists(DataSaveLoad.JsonPathSettings))
        //        {
        //            ObservableCollection<Settings> settings = new ObservableCollection<Settings>();
        //            settings = DataSaveLoad.LoadData<Settings>(DataSaveLoad.JsonPathSettings);

        //            for (int i = 0; i < TaskItems.Count; i++)
        //            {
        //                if (TaskItems[i].Noty)
        //                {
        //                    if (settings[0].Hours1)
        //                    {
        //                        if ((TaskItems[i].DateComplete - DateTime.Now).TotalMinutes < 60)
        //                        {
        //                            var notify1 = new ToastContentBuilder();
        //                            notify1.AddText($"Посмотри, до выполнения задачи {TaskItems[i].HeaderPlan}, осталось менее часа!");
        //                            notify1.AddAppLogoOverride(new Uri($"{Environment.CurrentDirectory}\\PLANSA.ico"));
        //                            notify1.Show();
        //                        }
        //                    }

        //                    if (settings[0].Hours3)
        //                    {
        //                        if ((TaskItems[i].DateComplete - DateTime.Now).TotalMinutes < 180)
        //                        {
        //                            var notify1 = new ToastContentBuilder();
        //                            notify1.AddText($"Посмотри, до выполнения задачи {TaskItems[i].HeaderPlan}, осталось менее 3х часов!");
        //                            notify1.AddAppLogoOverride(new Uri($"{Environment.CurrentDirectory}\\PLANSA.ico"));
        //                            notify1.Show();
        //                        }
        //                    }

        //                    if (settings[0].Hours5)
        //                    {
        //                        if ((TaskItems[i].DateComplete - DateTime.Now).TotalMinutes < 350)
        //                        {
        //                            var notify1 = new ToastContentBuilder();
        //                            notify1.AddText($"Посмотри, до выполнения задачи {TaskItems[i].HeaderPlan}, осталось менее 5-ти часов!");
        //                            notify1.AddAppLogoOverride(new Uri($"{Environment.CurrentDirectory}\\PLANSA.ico"));
        //                            notify1.Show();
        //                        }
        //                    }
        //                }
        //            }
        //            for (int i = 0; i < TaskItems.Count; i++)
        //            {
        //                if((TaskItems[i].DateComplete - DateTime.Now).TotalMinutes < 0)
        //                {
        //                    var notify1 = new ToastContentBuilder();
        //                    notify1.AddText($"Наш план: {TaskItems[i].HeaderPlan}, провален! Срочно предпринимай действия!");
        //                    notify1.AddAppLogoOverride(new Uri($"{Environment.CurrentDirectory}\\PLANSA.ico"));
        //                    notify1.Show();
        //                    TaskItems[i].Failed = true;
        //                    TaskItems[i].Status = "Просрочено";
        //                }
        //            }
        //        }
        //        Thread.Sleep(40000);
        //    }
        //}

        //public async void PushNotyAsync()
        //{
        //    await Task.Run(() => PushNoty());
        //}

        //#endregion

        //public static void SetAutorunValue(bool autorun)
        //{
        //    if(autorun)
        //    {
        //        var bootMeUp = new BootMeUp();

        //        bootMeUp.UseAlternativeOnFail = true;
        //        bootMeUp.RunWhenDebugging = true;
        //        bootMeUp.CreateShortcut();
        //        bootMeUp.BootArea = BootMeUp.BootAreas.StartupFolder;
        //        bootMeUp.BootArea = BootMeUp.BootAreas.Registry;
        //        bootMeUp.TargetUser = BootMeUp.TargetUsers.AllUsers;


        //        bootMeUp.Enabled = true;
        //    }
        //}      
    }
}
