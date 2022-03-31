using DirectoryManager.Services;
using DirectoryManager.Services.EventHandlers;
using static DirectoryManager.Services.FileSystemVisitor;

string path =  @"C:\Windows";

#region task1.1
//Directory.GetFiles(path).ToList().ForEach(x => Console.WriteLine(x));
//Directory.GetDirectories(path).ToList().ForEach(x => Console.WriteLine(x));
#endregion

#region task1.2
//implement lambda

//Filter _filter = fileslist =>
//{
//    return fileslist.Where(x => x.StartsWith(path + @"\d")).ToList();
//};

//FileSystemVisitor fileSystem = new(path, _filter);
//fileSystem.LogFilterFiles();
#endregion

#region task2.1
//var pub = new EventPublisher();
//pub.RaiseCustomEvent += Pub_RaiseCustomEvent;

//void Pub_RaiseCustomEvent(object? sender, CustomEventArgs e)
//{
//    Console.WriteLine("Notification!!!:"+e.Message);
//}

//pub.PublishEvent("Event triggered");
#endregion

#region task2.2
//Filter _filter = fileslist =>
//{
//    return fileslist.Where(x => x.StartsWith(path + @"\d")).ToList();
//};

//FileSystemVisitor fileSystem = new(path, _filter);
//FilterEventPublisher publisher = new(fileSystem);
//EventSubscriber subscriber1=new(1,publisher);
//publisher.Start();
//publisher.Finish();
#endregion

#region task2.3
Filter _filter = fileslist =>
{
    return fileslist.Where(x => x.StartsWith(path + @"\d")).ToList();
};

FileSystemVisitor fileSystem = new(path, _filter);
Publisher publisher = new(fileSystem);
FinishSubscriber Subscriber = new(1, publisher);
publisher.FileFound(true);
publisher.FilteredFileFound(true);
publisher.DirectoryFound();
publisher.FilteredDirectoryFound();

#endregion

Console.WriteLine("Press any key to continue...");
Console.ReadLine();
