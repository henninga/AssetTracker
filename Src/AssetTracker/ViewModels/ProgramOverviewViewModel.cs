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
    public class ProgramOverviewViewModel : Conductor<IScreen>.Collection.OneActive
    {
        readonly IWindowManager _windowManager;
        readonly IEventAggregator _events;
        public string Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }

        public ObservableCollection<VersionViewModel> Versions { get; set; }
        public VersionViewModel CurrentSelection { get; set; }

        public ProgramOverviewViewModel(IWindowManager windowManager, IEventAggregator events)
        {
            _windowManager = windowManager;
            _events = events;
            Versions = new ObservableCollection<VersionViewModel>();
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

        public IEnumerable<IResult> DeleteVersion()
        {
            if (!Show.ConfirmationDialog("Vil du slette version?", "Slett version"))
            {
                yield return new EmptyResult();
                yield break;
                
            }
            
            yield return Show.Busy();
            
            var deleteVersion = new DeleteVersion(Id, CurrentSelection.Version).AsCommand();
            yield return deleteVersion;
            var getProgram = new GetProgram(Id).AsQuery();
            yield return getProgram;
            yield return Show.Screen<ProgramOverviewViewModel>()
                                .Configured(x => x.WithData(getProgram.Response));
            yield return Show.NotBusy();
        }

        public IEnumerable<IResult> NewVersion()
        {
            var vm = new AddVersionViewModel();
            var result = _windowManager.ShowDialog(vm, null);

            if (result == true)
            {
                yield return Show.Busy();
                yield return new AddVersionToProgram(Id, vm.Input).AsCommand();
                var getProgram = new GetProgram(Id)
                    .AsQuery();
                yield return getProgram;
                yield return Show.Screen<ProgramOverviewViewModel>()
                                .Configured(x => x.WithData(getProgram.Response));
                yield return Show.NotBusy();
            }

            yield return new EmptyResult();
        }

        public IEnumerable<IResult> Edit()
        {
            yield return Show.Screen<EditProgramViewModel>()
                                .Configured(x => x.WithData(Id, Name, Notes));
        }

        public void WithData(Program source)
        {
            Id = source.Id;
            Name = source.Name;
            Notes = source.Notes;

            if (source.Versions == null)
                return;

            source.Versions.Each(x =>
            {
                var versionViewModel = new VersionViewModel(_windowManager)
                {
                    ProgramId = Id,
                    Version = x.Version,
                };

                if (x.Licenses != null)
                {
                    x.Licenses.Each(license => versionViewModel.Licenses.Add(new IndividualLicenseViewModel()
                                                                                      {
                                                                                          Version = x.Version,
                                                                                          ProgramId = source.Id,
                                                                                          Username = license.Username,
                                                                                          Key = license.Key
                                                                                      }));
                }
                Versions.Add(versionViewModel);
            });
        }
    }
}