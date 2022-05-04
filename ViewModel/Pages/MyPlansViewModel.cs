using PLANSA.Model;
using PLANSA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLANSA.ViewModel.Pages
{
    internal class MyPlansViewModel : Observer
    {

        public ObservableCollectionEX<TaskItem> TaskItems { get; set; }
        public MyPlansViewModel()
        {
            TaskItems = new ObservableCollectionEX<TaskItem>();
            TaskItems = DataSaveLoad.LoadJson();
        }
    }
}
