using System;
using System.Collections.Generic;
using System.Linq;
using AssetTracker.Model;
using AssetTracker.ViewModels;
using Raven.Client;
using Raven.Client.Linq;

namespace AssetTracker.Queries
{
    public class SearchPrograms : IQuery<IEnumerable<IndividualResultViewModel>>
    {
        readonly string _searchText;

        public SearchPrograms(string searchText)
        {
            _searchText = searchText;
        }

        public void Execute(IDocumentSession session, Action<IEnumerable<IndividualResultViewModel>> reply)
        {
            reply(
                session.Query<Program>()
                    .Where(x => x.Name.Contains(_searchText))
                    .Select(x => new IndividualResultViewModel(x.Id, x.Name))
                    .ToList()
                );
        }
    }
}