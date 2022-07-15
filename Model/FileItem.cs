using PLANSA.Services;

namespace PLANSA.Model
{
    public class FileItem : ObserverableObject
    {
        private string _files;

        public string files
        {
            get { return _files; }
            set { _files = value; OnPropertyChanged(); }
        }

        private string _nameFile;

        public string FileName
        {
            get => _nameFile;
            set { _nameFile = value; OnPropertyChanged(); }
        }

    }
}
