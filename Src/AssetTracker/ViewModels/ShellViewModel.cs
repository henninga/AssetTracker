using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using AssetTracker.Extensions;
using AssetTracker.Framework;
using AssetTracker.Messages;
using AssetTracker.Model;
using AssetTracker.Queries;
using Caliburn.Micro;

namespace AssetTracker.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>, IShell
    {
        readonly IEventAggregator _eventAggregator;
        readonly IScreen _startViewModel;

        public ShellViewModel(IEventAggregator eventAggregator, AllProgramsViewModel startViewModel)
        {
            _eventAggregator = eventAggregator;
            _startViewModel = startViewModel;
            DisplayName = "Asset Tracker";
        }

        public string SearchText { get; set; }
        public bool IsBusy { get; set; }

        public IEnumerable<IResult> Back()
        {
            yield return Show.Busy();
            yield return Show.Screen<AllProgramsViewModel>();
            yield return Show.NotBusy();
        }

        protected override void OnActivate()
        {
            ActivateItem(_startViewModel);
        }

        public IEnumerable<IResult> ExecuteSearch()
        {
            yield return Show.Busy();
            yield return Show.Screen<SearchResultsViewModel>();
            var search = new SearchPrograms(SearchText)
                                .AsResult();
            yield return search;

            var responseCount = search.Response.Count();
            if (responseCount == 0)
                _eventAggregator.Publish(SearchCompletedMessage.WithNoResults());
            else if (responseCount == 1 && string.Compare(search.Response.First().Name, SearchText,true) == 0)
            {
                var getProgram = new GetProgram(search.Response.First().Id)
                                    .AsResult();

                yield return getProgram;
                yield return Show.Screen<ProgramOverviewViewModel>()
                            .Configured(x => x.WithData(getProgram.Response));
            }
            else if (responseCount > 0)
                _eventAggregator.Publish(SearchCompletedMessage.WithResults(search.Response));

            yield return Show.NotBusy();
        }

        public bool CanExecuteSearch
        {
            get { return !string.IsNullOrEmpty(SearchText); }
        }
    }
}
