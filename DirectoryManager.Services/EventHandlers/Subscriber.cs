using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryManager.Services.EventHandlers
{
    public class Subscriber
    {
        private readonly string _id;

        public Subscriber(string id, EventPublisher pub)
        {
            _id = id;

            // Subscribe to the event
            pub.RaiseCustomEvent += HandleCustomEvent;
        }

        // Define what actions to take when the event is raised.
        void HandleCustomEvent(object sender, CustomEventArgs e)
        {
            Console.WriteLine($"{_id} received this message: {e.Message}");
        }
    }

    public class EventSubscriber
    {
        private readonly int _id;

        public EventSubscriber(int id, FilterEventPublisher pub)
        {
            _id = id;

            // Subscribe to the event
            pub.StartFilterEvent += (object? sender, FilterEventArgs e) =>
            {
                e.Visitor.FilterResult();
                Console.WriteLine($"For {_id} subscriber started filter event ...\n");
            };

            pub.FinishFilterEvent += (object? sender, FilterEventArgs e) =>
            {
                e.Visitor.LogResults();
                Console.WriteLine($"\nFor {_id} subscriber finished filter event");
            };
        }
    }
    public class FinishSubscriber
    {
        private readonly int _id;
        public FinishSubscriber(int id, Publisher pub)
        {
            _id = id;

            // Subscribe to the event
            pub.FileFoundEvent += (object? sender, FilterEventArgs e) =>
            {
                LogEventStatus(nameof(pub.FileFoundEvent));
                e.Visitor.ClearAllFoundFiles();
                e.Visitor.GetFiles();
                e.Visitor.LogResults();
                LogEventStatus(nameof(pub.FileFoundEvent),false);
            };

            pub.FilteredFileFoundEvent += (object? sender, FilterEventArgs e) =>
            {
                LogEventStatus(nameof(pub.FilteredFileFoundEvent));
                e.Visitor.ClearAllFoundFiles();
                e.Visitor.GetFiles();
                e.Visitor.FilterResult();
                e.Visitor.LogResults();
                LogEventStatus(nameof(pub.FilteredFileFoundEvent),false);
            };

            pub.DirectoryFoundEvent += (object? sender, FilterEventArgs e) =>
            {
                LogEventStatus(nameof(pub.DirectoryFoundEvent)); 
                e.Visitor.ClearAllFoundFiles();
                e.Visitor.GetDirectories();
                e.Visitor.LogResults();
                LogEventStatus(nameof(pub.DirectoryFoundEvent),false);
            };

            pub.FilteredDirectoryFoundEvent += (object? sender, FilterEventArgs e) =>
            {
                LogEventStatus(nameof(pub.FilteredDirectoryFoundEvent));
                e.Visitor.ClearAllFoundFiles();
                e.Visitor.GetDirectories();
                e.Visitor.FilterResult();
                e.Visitor.LogResults();
                LogEventStatus(nameof(pub.FilteredDirectoryFoundEvent),false);
            };

            void LogEventStatus(string NameEvent, bool isStart = true)
            {
                if (isStart) Console.WriteLine($"\n=====For {_id} subscriber started {NameEvent}.... =====\n");
                else Console.WriteLine($"\n=====For {_id} subscriber finished {NameEvent}=====\n");
            }

        }

    }
}
