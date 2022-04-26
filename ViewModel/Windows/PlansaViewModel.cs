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

        private int _index;

        public int SelectedIndex
        {
            get => _index;
            set { _index = value; OnPropertyChanged(); }
        }

        private int _numberPlan;

        public int  NumberPlan
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
            get  => _color; 
            set { _color = value; OnPropertyChanged(); }
        }

        public static readonly string pathTonumberPlan = $"{Environment.CurrentDirectory}\\numberPlan.txt";


        #region Commands
        public RelayCommand CloseApp { get; set; }
        public RelayCommand Maximize { get; set; }
        public RelayCommand Minimize { get; set; }
        public RelayCommand OpenFile { get; set; }
        public RelayCommand LeftPlan { get; set; }
        public RelayCommand RightPlan { get; set; }
        public RelayCommand OpenSettings { get; set; }
        public RelayCommand CreatePlanOpenWindow { get; set; }

        public ObservableCollection<FileItem> Files { get; set; }
        public ObservableCollectionEX<TaskItem> TaskItems { get; set; }
        #endregion

        public PlansaViewModel()
        {
            if(File.Exists(pathTonumberPlan))
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
            for (int i = 0; i < TaskItems[NumberPlan].files.Count; i++)
            {
                Files.Add(new FileItem() { files = TaskItems[NumberPlan].files[i] });
            }
            PlanContent = TaskItems[NumberPlan].PlanContent;
            DeadLine = TaskItems[NumberPlan].DateComplete;
            TimeOF = Math.Round((DeadLine - DateTime.Now).TotalHours, 1).ToString() + " Часов. ";
            CalculateDeadLine();

            Instance = this;

            #region Commands
            CloseApp = new RelayCommand(o =>
            {
                Application.Current.Shutdown();
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
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
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
                if(valuePlan < limitRight-1)
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
                }
            });

            OpenSettings = new RelayCommand(o =>
            {
                
            });
            #endregion
        }

        private void CalculateDeadLine()
        {
            double value;
            value = Math.Round((DeadLine - DateTime.Now).TotalHours, 1);
            if(value > 100)
            {
                ColorPriority = (Brush) new BrushConverter().ConvertFrom("#2EB872");
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
    }
}
