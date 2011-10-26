using System.Collections.ObjectModel;
using Caliburn.Micro;

namespace AssetTracker.ViewModels
{
    public class ProgramVersionViewModel : Screen
    {
        public string Version { get; set; }
        public string ProgramId { get; set; }

        public ObservableCollection<ProgramVersionLicenseViewModel> Licenses { get; set; }

        public ProgramVersionViewModel()
        {
            Licenses = new ObservableCollection<ProgramVersionLicenseViewModel>();
        }

        public void NewLicense()
        {
            
        }
    }
}