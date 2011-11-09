using System.Collections.Generic;
using System.Threading;
using AssetTracker.Commands;
using AssetTracker.DirectoryServices;
using AssetTracker.Framework;
using Caliburn.Micro;

namespace AssetTracker.ViewModels
{
    public class IndividualLicenseViewModel : Screen
    {
        public IndividualLicenseViewModel()
        {
        }

        public IndividualLicenseViewModel(string username, string key, string programId, string version, DirectoryUser assignedToUser)
        {
            Username = username;
            Key = key;
            ProgramId = programId;
            Version = version;
            AssignedToUser = assignedToUser;
        }

        public string Key { get; set; }
        public string Username { get; set; }
        public string ProgramId { get; set; }
        public string Version { get; set; }
        public DirectoryUser AssignedToUser { get; set; }

        public IEnumerable<IResult> AssignUser()
        {
            yield return Show.Busy();
            yield return Show.Screen<AssignUserViewModel>()
                             .Configured(x => x.WithData(Key, Username, ProgramId, Version, AssignedToUser));
            yield return Show.NotBusy();
        }

        public void DeleteLicense()
        {
        }

        public void CopyKey()
        {
            Key.ToClipboard();
        }

        public bool CanDeleteLicense
        {
            get { return true; }
        }

        public void EditLicense()
        {
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (IndividualLicenseViewModel)) return false;
            return Equals((IndividualLicenseViewModel) obj);
        }

        public bool Equals(IndividualLicenseViewModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Key, Key);
        }

        public override int GetHashCode()
        {
            return (Key != null ? Key.GetHashCode() : 0);
        }
    }
}