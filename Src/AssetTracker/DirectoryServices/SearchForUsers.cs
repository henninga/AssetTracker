using System;
using System.Collections.Generic;
using Caliburn.Micro;

namespace AssetTracker.DirectoryServices
{
    public class SearchForUsers : IDirectoryQuery<IObservableCollection<DirectoryUser>>
    {
        readonly string _searchText;

        public SearchForUsers(string searchText)
        {
            _searchText = searchText;
        }

        public void Execute(Action<IObservableCollection<DirectoryUser>> reply)
        {
            var directoryService = IoC.Get<IDirectoryService>();
            reply(
                directoryService.GetUsers(_searchText)
                );
        }
    }
}