using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AssetTracker.Commands;
using AssetTracker.Extensions;
using AssetTracker.Framework;
using AssetTracker.Messages;
using AssetTracker.Model;
using AssetTracker.Queries;
using Caliburn.Micro;
using Microsoft.Windows.Controls;

namespace AssetTracker.ViewModels
{
    public class ProgramVersionsViewModel : Conductor<IScreen>.Collection.OneActive
    {
        readonly IWindowManager _windowManager;
        readonly IEventAggregator _events;
        public string Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }

        public ObservableCollection<ProgramVersionViewModel> Versions { get; set; }

        public ProgramVersionsViewModel(IWindowManager windowManager, IEventAggregator events)
        {
            _windowManager = windowManager;
            _events = events;
            Versions = new ObservableCollection<ProgramVersionViewModel>();
        }

        protected override void OnActivate()
        {
            if (Versions == null)
                return;

            foreach (var version in Versions)
            {
                Items.Add(version);
            }
        }

        public IEnumerable<IResult> NewVersion()
        {
            var vm = new AddProgramVersionViewModel();
            var result = _windowManager.ShowDialog(vm, null, null);
            
            if(result == true)
            {
                yield return Show.Busy();
                yield return new AddVersionToProgram(Id, vm.Input).AsResult();
                var getProgram = new GetProgram(Id)
                    .AsResult();
                yield return getProgram;
                yield return Show.Screen<ProgramVersionsViewModel>()
                                .Configured(x => x.WithData(getProgram.Response));
                yield return Show.NotBusy();
            }
            
            yield return new EmptyResult();
        }

        public void WithData(Program source)
        {
            Id = source.Id;
            Name = source.Name;
            Notes = source.Notes;

            if (source.ProgramVersions == null)
                return;

            source.ProgramVersions.Each(x =>
            {
                var versionViewModel = new ProgramVersionViewModel()
                {
                    Version = x.Version,
                };

                if (x.VersionLicenseses != null)
                {
                    x.VersionLicenseses.Each(license => versionViewModel.Licenses.Add(new ProgramVersionLicenseViewModel()
                                                                                      {
                                                                                          Key = license.Key
                                                                                      }));
                }
                Versions.Add(versionViewModel);
            });
        }
    }
}