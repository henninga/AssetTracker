using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AssetTracker.Framework;
using AssetTracker.Queries;
using Caliburn.Micro;

namespace AssetTracker.ViewModels
{
    public class AllProgramsViewModel : Screen
    {
        public object Results { get; set; }

        public IEnumerator<IResult> GetAllPrograms()
        {
            var search = new GetAllPrograms()
                .AsQuery();
            yield return Show.Busy();
            yield return search;
            if (search.Response.Count() == 0)
                Results = new NoProgramsViewModel();
            else
                Results = new ResultsViewModel(new ObservableCollection<IndividualResultViewModel>(search.Response));

            yield return Show.NotBusy();
        }

        protected override void OnActivate()
        {
            Coroutine.BeginExecute(GetAllPrograms());
        }
    }
}