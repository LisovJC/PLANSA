using PLANSA.Services;

namespace PLANSA.Model
{
    internal class FileItem : Observer
    {
        private string _files;

        public string files
        {
            get { return _files; }
            set { _files = value; OnPropertyChanged(); }
        }
    }
}
