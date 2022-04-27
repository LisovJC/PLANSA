using Microsoft.Toolkit.Uwp.Notifications;
using PLANSA.Model;
using PLANSA.Services;
using PLANSA.View.Windows;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace PLANSA.ViewModel.Windows
{
    internal class PlansaViewModel : Observer
    {
        public static PlansaViewModel Instance { get; set; }

        private bool WindowStateFlag { get; set; } = true;

        #region ForFirstFlat
        private int _index;

        public int SelectedIndex
        {
            get => _index;
            set { _index = value; OnPropertyChanged(); }
        }

        private int _numberPlan;

        public int NumberPlan
        {
            get => _numberPlan;
            set { _numberPlan = value; OnPropertyChanged(); }
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

        public static readonly string pathTonumberPlan = $"{Environment.CurrentDirectory}\\numberPlan.txt";

        #endregion

        #region ForSecondFlat
        private int _index_2;

        public int SelectedIndex_2
        {
            get => _index_2;
            set { _index_2 = value; OnPropertyChanged(); }
        }

        private int _numberPlan_2;

        public int NumberPlan_2
        {
            get => _numberPlan_2;
            set { _numberPlan_2 = value; OnPropertyChanged(); }
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

        public static readonly string pathTonumberPlan_2 = $"{Environment.CurrentDirectory}\\numberPlan_2.txt";
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

        public ObservableCollection<FileItem> Files { get; set; }
        public ObservableCollectionEX<TaskItem> TaskItems { get; set; }

        public ObservableCollection<FileItem> Files_2 { get; set; }
        #endregion

        private Brush _clipColor_1 = (Brush)new BrushConverter().ConvertFrom("#0000");

        public Brush ClipColor_1
        {
            get => _clipColor_1;
            set { _clipColor_1 = value; OnPropertyChanged(); }
        }

        private Brush _clipColor_2 = (Brush)new BrushConverter().ConvertFrom("#0000");

        public Brush ClipColor_2
        {
            get => _clipColor_2;
            set { _clipColor_2 = value; OnPropertyChanged(); }
        }

        private bool _clip_1 = true;

        public bool Clip_1
        {
            get => _clip_1;
            set { _clip_1 = value; OnPropertyChanged(); }
        }

        private bool _clip_2 = true;

        public bool Clip_2
        {
            get => _clip_2;
            set { _clip_2 = value; OnPropertyChanged(); }
        }
       
        public static readonly string Clipping_1 = $"{Environment.CurrentDirectory}\\Clipping_1.txt";
        public static readonly string Clipping_2 = $"{Environment.CurrentDirectory}\\Clipping_2.txt";

        public static readonly string Color_1 = $"{Environment.CurrentDirectory}\\Color_1.txt";
        public static readonly string Color_2 = $"{Environment.CurrentDirectory}\\Color_2.txt";


        public PlansaViewModel()
        {
            #region Flat_1
            if (File.Exists(pathTonumberPlan))
            {
                NumberPlan = int.Parse(File.ReadAllText(pathTonumberPlan));
                if (NumberPlan == -1)
                {
                    NumberPlan = 0;
                }
            }
            else
            {
                File.WriteAllText(pathTonumberPlan, "0");
                NumberPlan = 0;
            }


            Files = new ObservableCollection<FileItem>();
            TaskItems = new ObservableCollectionEX<TaskItem>();
            TaskItems = DataSaveLoad.LoadJson();
            if (TaskItems.Count > 0)
            {
                for (int i = 0; i < TaskItems[NumberPlan].files.Count; i++)
                {
                    Files.Add(new FileItem() { files = TaskItems[NumberPlan].files[i] });
                }
                PlanContent = TaskItems[NumberPlan].PlanContent;
                DeadLine = TaskItems[NumberPlan].DateComplete;
                TimeOF = Math.Round((DeadLine - DateTime.Now).TotalHours, 1).ToString() + " Часов. ";
                CalculateDeadLine();
            }
            #endregion

            #region Flat_2
            Files_2 = new ObservableCollection<FileItem>();
            if (File.Exists(pathTonumberPlan_2))
            {
                NumberPlan_2 = int.Parse(File.ReadAllText(pathTonumberPlan_2));
                if (NumberPlan_2 == -1)
                {
                    NumberPlan_2 = 0;
                }
            }
            else
            {
                File.WriteAllText(pathTonumberPlan_2, "0");
                NumberPlan_2 = 0;
            }
            
            TaskItems = new ObservableCollectionEX<TaskItem>();
            TaskItems = DataSaveLoad.LoadJson();
            if (TaskItems.Count > 0)
            {
                for (int i = 0; i < TaskItems[NumberPlan_2].files.Count; i++)
                {
                    Files_2.Add(new FileItem() { files = TaskItems[NumberPlan_2].files[i] });
                }
                PlanContent_2 = TaskItems[NumberPlan_2].PlanContent;
                DeadLine_2 = TaskItems[NumberPlan_2].DateComplete;
                TimeOF_2 = Math.Round((DeadLine_2 - DateTime.Now).TotalHours, 1).ToString() + " Часов. ";
                CalculateDeadLine_2();
                #endregion

            #region Clips
                if (File.Exists(Clipping_1))
                {
                    Clip_1 = bool.Parse(File.ReadAllText(Clipping_1));
                }
                else
                {
                    Clip_1 = true;
                    File.WriteAllText(Clipping_1, "false");
                }

                if (File.Exists(Clipping_2))
                {
                    Clip_2 = bool.Parse(File.ReadAllText(Clipping_2));
                }
                else
                {
                    Clip_2 = true;
                    File.WriteAllText(Clipping_2, "false");
                }

                
                if (File.Exists(Color_1))
                {
                    ClipColor_1 = (Brush)new BrushConverter().ConvertFrom(File.ReadAllText(Color_1));
                }
                else
                {
                    ClipColor_1 = (Brush)new BrushConverter().ConvertFrom("#0D7377");
                    File.WriteAllText(Color_1, "#0D7377");
                }

                if (File.Exists(Color_2))
                {
                    ClipColor_2 = (Brush)new BrushConverter().ConvertFrom(File.ReadAllText(Color_2));
                }
                else
                {
                    ClipColor_2 = (Brush)new BrushConverter().ConvertFrom("#0D7377");
                    File.WriteAllText(Color_2, "#0D7377");
                }
                #endregion


                Instance = this;

                #region Commands
                CloseApp = new RelayCommand(o =>
                {
                    Application.Current.Shutdown();
                });

                ClearAll = new RelayCommand(o =>
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
                    ClearDeadLine();

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
                    int valuePlan = int.Parse(File.ReadAllText(pathTonumberPlan)), limitRight = TaskItems.Count;
                    if (valuePlan < limitRight - 1)
                    {
                        valuePlan++;
                        File.WriteAllText(pathTonumberPlan, valuePlan.ToString());

                        TaskItems = DataSaveLoad.LoadJson();
                        Files.Clear();
                        for (int i = 0; i < TaskItems[valuePlan].files.Count; i++)
                        {
                            Files.Add(new FileItem() { files = TaskItems[valuePlan].files[i] });
                        }
                        PlanContent = TaskItems[valuePlan].PlanContent;
                        DeadLine = TaskItems[valuePlan].DateComplete;
                        TimeOF = Math.Round((DeadLine - DateTime.Now).TotalHours, 1).ToString() + " Часов. ";
                        CalculateDeadLine();
                        TaskItems = DataSaveLoad.LoadJson();
                    }
                });

                LeftPlan = new RelayCommand(o =>
                {
                    int valuePlan = int.Parse(File.ReadAllText(pathTonumberPlan));
                    if (valuePlan > 0)
                    {
                        valuePlan--;
                        File.WriteAllText(pathTonumberPlan, valuePlan.ToString());

                        TaskItems = DataSaveLoad.LoadJson();
                        Files.Clear();
                        for (int i = 0; i < TaskItems[valuePlan].files.Count; i++)
                        {
                            Files.Add(new FileItem() { files = TaskItems[valuePlan].files[i] });
                        }
                        PlanContent = TaskItems[valuePlan].PlanContent;
                        DeadLine = TaskItems[valuePlan].DateComplete;
                        TimeOF = Math.Round((DeadLine - DateTime.Now).TotalHours, 1).ToString() + " Часов. ";
                        CalculateDeadLine();
                        TaskItems = DataSaveLoad.LoadJson();
                    }
                });

                RightPlan_2 = new RelayCommand(o =>
                {
                    int valuePlan = int.Parse(File.ReadAllText(pathTonumberPlan_2)), limitRight = TaskItems.Count;
                    if (valuePlan < limitRight - 1)
                    {
                        valuePlan++;
                        File.WriteAllText(pathTonumberPlan_2, valuePlan.ToString());

                        TaskItems = DataSaveLoad.LoadJson();
                        Files_2.Clear();
                        for (int i = 0; i < TaskItems[valuePlan].files.Count; i++)
                        {
                            Files_2.Add(new FileItem() { files = TaskItems[valuePlan].files[i] });
                        }
                        PlanContent_2 = TaskItems[valuePlan].PlanContent;
                        DeadLine_2 = TaskItems[valuePlan].DateComplete;
                        TimeOF_2 = Math.Round((DeadLine_2 - DateTime.Now).TotalHours, 1).ToString() + " Часов. ";
                        CalculateDeadLine_2();
                        TaskItems = DataSaveLoad.LoadJson();
                    }
                });

                LeftPlan_2 = new RelayCommand(o =>
                {
                    int valuePlan = int.Parse(File.ReadAllText(pathTonumberPlan_2));
                    if (valuePlan > 0)
                    {
                        valuePlan--;
                        File.WriteAllText(pathTonumberPlan_2, valuePlan.ToString());

                        TaskItems = DataSaveLoad.LoadJson();
                        Files_2.Clear();
                        for (int i = 0; i < TaskItems[valuePlan].files.Count; i++)
                        {
                            Files_2.Add(new FileItem() { files = TaskItems[valuePlan].files[i] });
                        }
                        PlanContent_2 = TaskItems[valuePlan].PlanContent;
                        DeadLine_2 = TaskItems[valuePlan].DateComplete;
                        TimeOF_2 = Math.Round((DeadLine_2 - DateTime.Now).TotalHours, 1).ToString() + " Часов. ";
                        CalculateDeadLine_2();
                        TaskItems = DataSaveLoad.LoadJson();

                    }
                });

                ClipPlan_1 = new RelayCommand(o =>
                {
                    if(Clip_1)
                    {
                        Clip_1 = false;
                        File.WriteAllText(Clipping_1, "false");
                        ClipColor_1 = (Brush)new BrushConverter().ConvertFrom("#E23E57");
                        File.WriteAllText(Color_1, "#E23E57");
                    }
                    else
                    {
                        Clip_1 = true;
                        File.WriteAllText(Clipping_1, "true");
                        ClipColor_1 = (Brush)new BrushConverter().ConvertFrom("#0D7377");
                        File.WriteAllText(Color_1, "#0D7377");
                    }
                });

                ClipPlan_2 = new RelayCommand(o =>
                {
                    if (Clip_2)
                    {
                        Clip_2 = false;
                        File.WriteAllText(Clipping_2, "false");
                        ClipColor_2 = (Brush)new BrushConverter().ConvertFrom("#E23E57");
                        File.WriteAllText(Color_2, "#E23E57");
                    }
                    else
                    {
                        Clip_2 = true;
                        File.WriteAllText(Clipping_2, "true");
                        ClipColor_2 = (Brush)new BrushConverter().ConvertFrom("#0D7377");
                        File.WriteAllText(Color_2, "#0D7377");
                    }
                });

                OpenSettings = new RelayCommand(o =>
                {

                });
                #endregion
            }

             void CalculateDeadLine()
            {
                double value;
                value = Math.Round((DeadLine - DateTime.Now).TotalHours, 1);
                if (value > 100)
                {
                    ColorPriority = (Brush)new BrushConverter().ConvertFrom("#2EB872");
                }

                if (value < 100 && value > 48)
                {
                    ColorPriority = (Brush)new BrushConverter().ConvertFrom("#F7F48B");
                }

                if (value < 48 && value > 24)
                {
                    ColorPriority = (Brush)new BrushConverter().ConvertFrom("#F38181");
                }

                if (value < 24)
                {
                    ColorPriority = (Brush)new BrushConverter().ConvertFrom("#A40A3C");
                }
            }

             void CalculateDeadLine_2()
            {
                double value;
                value = Math.Round((DeadLine_2 - DateTime.Now).TotalHours, 1);
                if (value > 100)
                {
                    ColorPriority_2 = (Brush)new BrushConverter().ConvertFrom("#2EB872");
                }

                if (value < 100 && value > 48)
                {
                    ColorPriority_2 = (Brush)new BrushConverter().ConvertFrom("#F7F48B");
                }

                if (value < 48 && value > 24)
                {
                    ColorPriority_2 = (Brush)new BrushConverter().ConvertFrom("#F38181");
                }

                if (value < 24)
                {
                    ColorPriority_2 = (Brush)new BrushConverter().ConvertFrom("#A40A3C");
                }
            }

            void ClearDeadLine()
            {                            
                ColorPriority = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
                ColorPriority_2 = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
            }
        }
    }
}
