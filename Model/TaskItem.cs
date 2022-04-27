using PLANSA.Services;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace PLANSA.Model
{
    public class TaskItem : Observer
    {
        private string _headerPlan;

        public string HeaderPlan
        {
            get => _headerPlan;
            set { _headerPlan = value; OnPropertyChanged(); }
        }

        private DateTime _dateAdd;

        public DateTime DateAdd
        {
            get => _dateAdd;
            set { _dateAdd = value; OnPropertyChanged(); }
        }

        private string _planContent;

        public string PlanContent
        {
            get => _planContent;
            set { _planContent = value; OnPropertyChanged(); }
        }


        private string _status;

        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }

        private SolidColorBrush _colorPriority;

        public SolidColorBrush ColorPriority
        {
            get => _colorPriority;
            set { _colorPriority = value; OnPropertyChanged(); }
        }

        private bool _Failed;

        public bool Failed
        {
            get => _Failed;
            set { _Failed = value; OnPropertyChanged(); }
        }

        private DateTime _dateComplete;

        public DateTime DateComplete
        {
            get => _dateComplete;
            set { _dateComplete = value; OnPropertyChanged(); }
        }

        private List<string> _files;

        public List<string> files
        {
            get { return _files; }
            set { _files = value; OnPropertyChanged(); }
        }
    }
}
