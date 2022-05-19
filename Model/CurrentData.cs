using PLANSA.Services;

namespace PLANSA.Model
{
    public class CurrentData : Observer
    {
        private int _selectedPlan_1;

        public int SelectedPlan_1
        {
            get =>_selectedPlan_1; 
            set { _selectedPlan_1 = value; OnPropertyChanged(); }
        }

        private int _selectedPlan_2;

        public int SelectedPlan_2
        {
            get => _selectedPlan_2;
            set { _selectedPlan_2 = value; OnPropertyChanged(); }
        }

        private bool _clipping_1;

        public bool Clipping_1
        {
            get => _clipping_1;
            set { _clipping_1 = value; OnPropertyChanged(); }
        }

        private bool _clipping_2;

        public bool Clipping_2
        {
            get => _clipping_2;
            set { _clipping_2 = value; OnPropertyChanged(); }
        }

        private string _color_1;

        public string Color_1
        {
            get => _color_1;
            set { _color_1 = value; OnPropertyChanged(); }
        }

        private string _color_2;

        public string Color_2
        {
            get => _color_2;
            set { _color_2 = value; OnPropertyChanged(); }
        }
    }
}