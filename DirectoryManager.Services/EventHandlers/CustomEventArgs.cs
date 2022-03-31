using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryManager.Services.EventHandlers
{
    public class CustomEventArgs : EventArgs
    {
        public CustomEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }

    public class FilterEventArgs : EventArgs
    {
        public FilterEventArgs(FileSystemVisitor visitor)
        {
            Visitor = visitor;
        }

        public FileSystemVisitor Visitor { get; set; }
    }
}
