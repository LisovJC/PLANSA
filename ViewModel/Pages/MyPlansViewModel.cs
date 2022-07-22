using PLANSA.Model;
using PLANSA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLANSA.ViewModel.Pages
{
    internal class MyPlansViewModel : ObserverableObject
    {

        public ObservableCollectionEX<TaskItem> TaskItems { get; set; }
        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

        public MyPlansViewModel()
        {
            TaskItems = new ObservableCollectionEX<TaskItem>();
            TaskItems = DataSaveLoad.LoadData<TaskItem>(DataSaveLoad.JsonPathTasks);
        }
    }
}
