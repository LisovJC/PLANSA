using PLANSA.Services;

namespace PLANSA.Model
{
    internal class CheckBoxItem : Observer
    {
        private string _textCheckBox;

        public string textCheckBox
        {
            get  => _textCheckBox;
            set { _textCheckBox = value; OnPropertyChanged(); }
        }

        private bool _isCheck;

        public bool isCheck
        {
            get => _isCheck; 
            set { _isCheck = value; OnPropertyChanged(); }
        }



    }
}
