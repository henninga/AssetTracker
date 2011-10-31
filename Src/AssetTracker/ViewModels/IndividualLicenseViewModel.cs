using System.Diagnostics;
using System.Windows;
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



        public void AssignUser()
        {

        }

        public void DeleteLicense()
        {

        }

        public void CopyKey()
        {
            Clipboard.SetText(Key);
            
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