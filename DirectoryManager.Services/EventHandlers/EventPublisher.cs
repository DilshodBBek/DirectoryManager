using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryManager.Services.EventHandlers
{
    public class EventPublisher
    {
        public event EventHandler<CustomEventArgs> RaiseCustomEvent;

        public void PublishEvent(string message)
        {
            OnRaiseCustomEvent(new CustomEventArgs(message));
        }
        protected virtual void OnRaiseCustomEvent(CustomEventArgs e)
        {
            EventHandler<CustomEventArgs> raiseEvent = RaiseCustomEvent;

            if (raiseEvent != null)
            {
                raiseEvent(this, e);
                RaiseCustomEvent += EventPublisher_RaiseCustomEvent;
            }
        }

        private void EventPublisher_RaiseCustomEvent(object? sender, CustomEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
    public class FilterEventPublisher
    {
        public event EventHandler<FilterEventArgs> StartFilterEvent;
        public event EventHandler<FilterEventArgs> FinishFilterEvent;

        public FileSystemVisitor _fileSystemVisitor { get; set; }

        public FilterEventPublisher(FileSystemVisitor fileSystemVisitor)
        {
            _fileSystemVisitor = fileSystemVisitor;
        }

        public void Start()
        {
            OnStartFilterEvent(new FilterEventArgs(_fileSystemVisitor));
        }     

        protected virtual void OnStartFilterEvent(FilterEventArgs e)
        {
            EventHandler<FilterEventArgs> startFilterEvent = StartFilterEvent;

            if (startFilterEvent != null)
            {
                startFilterEvent.Invoke(this, e);
            }
        }

        public void Finish()
        {
            OnFinishFilterEvent(new FilterEventArgs(_fileSystemVisitor));
        }
        protected virtual void OnFinishFilterEvent(FilterEventArgs e)
        {
            EventHandler<FilterEventArgs> finishFilterEvent = FinishFilterEvent;

            if (finishFilterEvent != null)
            {
                finishFilterEvent.Invoke(this, e);
            }
        }
    }

    public class Publisher
    {
        public event EventHandler<FilterEventArgs> FileFoundEvent;
        public event EventHandler<FilterEventArgs> FilteredFileFoundEvent;
        public event EventHandler<FilterEventArgs> DirectoryFoundEvent;
        public event EventHandler<FilterEventArgs> FilteredDirectoryFoundEvent;

        public FileSystemVisitor _fileSystemVisitor { get; set; }

        public Publisher(FileSystemVisitor fileSystemVisitor )
        {
            _fileSystemVisitor = fileSystemVisitor;
        }

        public void FileFound(bool IsAbortSearch = false)
        {
            if (!IsAbortSearch)
            OnFileFoundEvent(new FilterEventArgs(_fileSystemVisitor));
            else WarningLog(nameof(FileFoundEvent));

            
        }
        void WarningLog(string EventName)
        {
            Console.WriteLine($"Warning!!!:The {EventName} process stopped the reason why AbortSearch is true\n");
        }

        protected virtual void OnFileFoundEvent(FilterEventArgs e)
        {
            EventHandler<FilterEventArgs> fileFoundEvent = FileFoundEvent;

            if (fileFoundEvent != null)
            {
                fileFoundEvent.Invoke(this, e);
            }
        }

        public void FilteredFileFound(bool IsAbortSearch = false)
        {
            if (!IsAbortSearch)
                OnFilteredFileFoundEvent(new FilterEventArgs(_fileSystemVisitor));
            else WarningLog(nameof(FilteredFileFoundEvent));
        }
        protected virtual void OnFilteredFileFoundEvent(FilterEventArgs e)
        {
            EventHandler<FilterEventArgs> filteredFileFoundEvent = FilteredFileFoundEvent;

            if (filteredFileFoundEvent != null)
            {
                filteredFileFoundEvent.Invoke(this, e);
            }
        }

        public void DirectoryFound(bool IsAbortSearch = false)
        {
            if (!IsAbortSearch)
                OnDirectoryFoundEvent(new FilterEventArgs(_fileSystemVisitor));
            else WarningLog(nameof(DirectoryFoundEvent));
        }
        protected virtual void OnDirectoryFoundEvent(FilterEventArgs e)
        {
            EventHandler<FilterEventArgs> directoryFoundEvent = DirectoryFoundEvent;

            if (directoryFoundEvent != null)
            {
                directoryFoundEvent.Invoke(this, e);
            }
        }

        public void FilteredDirectoryFound(bool IsAbortSearch = false)
        {
            if (!IsAbortSearch)
                OnFilteredDirectoryFoundEvent(new FilterEventArgs(_fileSystemVisitor));
            else WarningLog(nameof(FilteredDirectoryFoundEvent));
        }

        protected virtual void OnFilteredDirectoryFoundEvent(FilterEventArgs e)
        {
            EventHandler<FilterEventArgs> filteredDirectoryFoundEvent = FilteredDirectoryFoundEvent;

            if (filteredDirectoryFoundEvent != null)
            {
                filteredDirectoryFoundEvent.Invoke(this, e);
            }
        }


    }
}
