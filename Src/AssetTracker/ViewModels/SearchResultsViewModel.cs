using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using AssetTracker.Messages;
using Caliburn.Micro;

namespace AssetTracker.ViewModels
{
    public class SearchResultsViewModel : Screen, IHandle<SearchCompletedMessage>
    {
        readonly IEventAggregator _eventAggregator;

        public SearchResultsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            
        }

        public object Results { get; set; }

        protected override void OnActivate()
        {
            _eventAggregator.Subscribe(this);
        }

        //protected override void OnDeactivate(bool close)
        //{
        //    _eventAggregator.Unsubscribe(this);
        //}



        public void Handle(SearchCompletedMessage message)
        {
            Results = message.Results;
        }

       
        
    }
}