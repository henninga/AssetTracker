using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using AssetTracker.Commands;
using AssetTracker.Extensions;
using AssetTracker.Framework;
using Caliburn.Micro;

namespace AssetTracker.ViewModels
{
    public class IndividualLicenseViewModel : Screen
    {
        public IndividualLicenseViewModel()
        {
        }

        public IndividualLicenseViewModel(string username, string key, string programId, string version)
        {
            Username = username;
            Key = key;
            ProgramId = programId;
            Version = version;
        }

        public string Key { get; set; }
        public string Username { get; set; }
        public string ProgramId { get; set; }
        public string Version { get; set; }



        public IEnumerable<IResult> AssignUser()
        {
            yield return Show.Busy();
            var assignUser = new AssignUser(ProgramId, Version, Username, Key, Thread.CurrentPrincipal.Identity.Name).AsCommand();
            yield return assignUser;
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
    }
}