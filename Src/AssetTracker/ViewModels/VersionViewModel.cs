using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AssetTracker.Commands;
using AssetTracker.Extensions;
using AssetTracker.Framework;
using AssetTracker.Queries;
using Caliburn.Micro;

namespace AssetTracker.ViewModels
{
    public class VersionViewModel : Screen
    {
        readonly IWindowManager _windowManager;
        public string Version { get; set; }
        public string ProgramId { get; set; }

        public ObservableCollection<IndividualLicenseViewModel> Licenses { get; set; }

        public VersionViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            Licenses = new ObservableCollection<IndividualLicenseViewModel>();
        }

        public IEnumerable<IResult> NewLicense()
        {


            var vm = new AddLicenseViewModel();
            var result = _windowManager.ShowDialog(vm, null);

            if (result == true)
            {
                yield return Show.Busy();
                yield return new AddLicenseToProgram(ProgramId, Version, vm.Username, vm.Key).AsCommand();
                Licenses.Add(new IndividualLicenseViewModel(vm.Username, vm.Key, ProgramId, Version));
                yield return Show.NotBusy();
            }

            yield return new EmptyResult();

        }
    }
}