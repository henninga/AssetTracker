using System;
using Caliburn.Micro;

namespace AssetTracker.DirectoryServices
{
    public class AllUsers : IDirectoryQuery<IObservableCollection<DirectoryUser>>
    {
        public void Execute(Action<IObservableCollection<DirectoryUser>> reply)
        {
            var directoryService = IoC.Get<IDirectoryService>();
            reply(
                directoryService.GetUsers()
                );
        }
    }
}