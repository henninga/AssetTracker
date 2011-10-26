using System;
using System.Collections.ObjectModel;
using Caliburn.Micro;

namespace AssetTracker.ViewModels
{
    public class ResultsViewModel : Screen
    {
        public ResultsViewModel(ObservableCollection<IndividualResultViewModel> results)
        {
            Results = results;
        }

        public ObservableCollection<IndividualResultViewModel> Results { get; set; }
    }
}