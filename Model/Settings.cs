using PLANSA.Services;

namespace PLANSA.Model
{
    public class Settings : Observer
    {
        private bool _5hours;

        public bool Hours5
        {
            get => _5hours;
            set { _5hours = value; OnPropertyChanged(); }
        }

        private bool _3hours;

        public bool Hours3
        {
            get => _3hours;
            set { _3hours = value; OnPropertyChanged(); }
        }

        private bool _1hours;

        public bool Hours1
        {
            get => _1hours;
            set { _1hours = value; OnPropertyChanged(); }
        }

        private bool _pushOn;

        public bool PushOn
        {
            get => _pushOn;
            set { _pushOn = value; OnPropertyChanged(); }
        }



    }
}
