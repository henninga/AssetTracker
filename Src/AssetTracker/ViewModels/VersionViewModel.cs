using System.Collections.ObjectModel;
using Caliburn.Micro;

namespace AssetTracker.ViewModels
{
    public class VersionViewModel : Screen
    {
        public string Version { get; set; }
        public string ProgramId { get; set; }

        public ObservableCollection<IndividualLicenseViewModel> Licenses { get; set; }

        public VersionViewModel()
        {
            Licenses = new ObservableCollection<IndividualLicenseViewModel>();
        }

        public void NewLicense()
        {
            
        }
    }
}