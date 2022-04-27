using PLANSA.Services;

namespace PLANSA.Model
{
    public class FileItem : Observer
    {
        private string _files;

        public string files
        {
            get { return _files; }
            set { _files = value; OnPropertyChanged(); }
        }
    }
}
