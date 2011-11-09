using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;
using AssetTracker.Commands;
using AssetTracker.DirectoryServices;
using AssetTracker.Framework;
using AssetTracker.Queries;
using Caliburn.Micro;
using Raven.Client;

namespace AssetTracker.ViewModels
{
    public class AssignUserViewModel : Screen
    {

        public string Key { get; set; }
        public string Username { get; set; }
        public string ProgramId { get; set; }
        public string Version { get; set; }
        public IObservableCollection<DirectoryUser> Users { get; set; }
        public DirectoryUser SelectedUser { get; set; }

        public IEnumerable<IResult> AssignUser()
        {
            var assignUser = new AssignUser(ProgramId, Version, Username, Key, SelectedUser).AsCommand();

            yield return assignUser;
        }

        public bool CanAssignUser { get { return SelectedUser != null; } }


        public string SearchText { get; set; }
        public bool CanExecuteSearch { get { return !string.IsNullOrWhiteSpace(SearchText); } }

        public IEnumerable<IResult> ExecuteSearch()
        {
            yield return Show.Busy();

            var searchForUsers = new SearchForUsers(SearchText).AsQuery();
            yield return searchForUsers;
            Users = searchForUsers.Response;
            yield return Show.NotBusy();
        }

        IEnumerator<IResult> GetAllUsers()
        {
            yield return Show.Busy();
            var allUsers = new AllUsers().AsQuery();
            yield return allUsers;
            Users = allUsers.Response;
            yield return Show.NotBusy();
        }

        protected override void OnActivate()
        {
            Coroutine.BeginExecute(GetAllUsers());
        }


        public void WithData(string key, string username, string programId, string version, DirectoryUser currentUser)
        {
            Key = key;
            Username = username;
            ProgramId = programId;
            Version = version;
            SelectedUser = currentUser;
        }
    }
}