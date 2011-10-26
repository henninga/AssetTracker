using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AssetTracker.ViewModels;
using Caliburn.Micro;

namespace AssetTracker.Messages
{
    public class SearchCompletedMessage
    {
        private SearchCompletedMessage(object results)
        {
            Results = results;
        }

        public object Results { get; set; }

        public static SearchCompletedMessage WithResults(IEnumerable<IndividualResultViewModel> results)
        {
            var observableResults = new ObservableCollection<IndividualResultViewModel>(results);
            return new SearchCompletedMessage(new ResultsViewModel(observableResults));
        }

        public static SearchCompletedMessage WithNoResults()
        {
            return new SearchCompletedMessage(new NoResultsViewModel());
        }
    }
}