namespace DirectoryManager.Services
{
    public class FileSystemVisitor
    {
        private string _path { get; set; }
        private List<string> _files = new List<string>();
        public delegate List<string> Filter(List<string> listFiles);
        Filter _SortFunction = x => x;
        public FileSystemVisitor(string path, Filter pointer)
        {
            _path = path;
            if (pointer != null)
            {
                _SortFunction = pointer;
            }

        }
        public void ClearAllFoundFiles()
        {
            _files.Clear();
        }
        //public void AbortSearch()
        //{
        //    _path = @"";
        //}
        public void LogResults()
        {
            foreach (var item in _files)
            {
                Console.WriteLine(item);
            }
        }

        public void FilterResult()
        {
            _files= _SortFunction(_files);
        }
        public void GetFiles()
        {
            _files.AddRange(Directory.GetFiles(_path));
        }
        public void GetDirectories()
        {
            _files.AddRange(Directory.GetDirectories(_path));
        }

    }
}
