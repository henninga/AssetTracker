using System.Collections.Generic;
using Caliburn.Micro;

namespace AssetTracker.DirectoryServices
{
    public interface IDirectoryService
    {
        IObservableCollection<DirectoryUser> GetUsers(string username);
        IObservableCollection<DirectoryUser> GetUsers();
    }
}