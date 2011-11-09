using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using Caliburn.Micro;

namespace AssetTracker.DirectoryServices
{
    public class DirectoryService : IDirectoryService
    {
        readonly DirectorySettings _settings;

        public DirectoryService(DirectorySettings settings)
        {
            _settings = settings;
        }

        public IObservableCollection<DirectoryUser> GetUsers(string username)
        {
            var currentUser = UserPrincipal.Current;
            if (equals(currentUser.SamAccountName, username) || equals(currentUser.UserPrincipalName, username) || equals(currentUser.Name, username))
                return new BindableCollection<DirectoryUser>()
                       {
                           new DirectoryUser(currentUser.UserPrincipalName, currentUser.Name)
                       };

            var results = _getUsers(string.Format("(&(|(cn={0}*)(sAMAccountName={0}*)(userPrincipalName={0}*))(ObjectCategory=Person))", username));

            return results;
        }

        IObservableCollection<DirectoryUser> _getUsers(string filter)
        {
            var entry = new DirectoryEntry("LDAP://" + _settings.LdapPath);
            var ds = new DirectorySearcher(entry)
                     {
                         Filter = filter,
                         SearchScope = SearchScope.OneLevel
                     };

            var results = new BindableCollection<DirectoryUser>();

            using (entry)
            using (ds)
            {
                var searchResults = ds.FindAll();

                foreach (SearchResult searchResult in searchResults)
                {
                    var userPrincipalName = searchResult.Properties["userPrincipalName"];
                    var fullname = searchResult.Properties["cn"];

                    results.Add(new DirectoryUser(userPrincipalName.Count > 0 ? (string)userPrincipalName[0] : "", fullname.Count > 0 ? (string)fullname[0] : ""));
                }
            }
            return results;
        }

        public IObservableCollection<DirectoryUser> GetUsers()
        {
            var results = _getUsers("(ObjectCategory=Person)");
            return results;
        }

        private static bool equals(string value1, string value2)
        {
            return String.Equals(value1, value2, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}