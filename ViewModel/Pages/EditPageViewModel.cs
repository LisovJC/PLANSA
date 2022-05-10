using PLANSA.Model;
using PLANSA.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLANSA.ViewModel.Pages
{
    internal class EditPageViewModel : Observer
    {
        public ObservableCollection<CheckBoxItem> checkBoxes { get; set; }

        public RelayCommand AddCheckBox { get; set; }

        public EditPageViewModel()
        {
            checkBoxes = new ObservableCollection<CheckBoxItem>();

            AddCheckBox = new RelayCommand(o =>
            {
                checkBoxes.Add(new CheckBoxItem() { MyProperty = true});
            });
        }
    }
}
