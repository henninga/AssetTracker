using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AssetTracker.Framework;
using AssetTracker.Messages;
using AssetTracker.Queries;
using Caliburn.Micro;

namespace AssetTracker.ViewModels
{
    public class ProgramSearchViewModel : Screen
    {
        public string SearchText { get; set; }
        public object Results { get; set; }

        public IEnumerable<IResult> ExecuteSearch()
        {
            yield return Show.Busy();
            var search = new SearchPrograms(SearchText)
                .AsQuery();
            yield return search;

            var responseCount = search.Response.Count();
            if (responseCount == 0)
                Results = new NoResultsViewModel();

            else if (responseCount == 1 && string.Compare(search.Response.First().Name, SearchText, true) == 0)
            {
                var getProgram = new GetProgram(search.Response.First().Id)
                    .AsQuery();

                yield return getProgram;
                yield return Show.Screen<ProgramOverviewViewModel>()
                    .Configured(x => x.WithData(getProgram.Response));
            }
            else if (responseCount > 0)
                Results = new ResultsViewModel(new ObservableCollection<IndividualResultViewModel>(search.Response));

            yield return Show.NotBusy();
        }

        protected override void OnActivate()
        {
            var program = new AllProgramsViewModel();
            program.ActivateWith(this);
            Results = program;
        }

        public bool CanExecuteSearch
        {
            get { return !string.IsNullOrEmpty(SearchText); }
        }
    }
}