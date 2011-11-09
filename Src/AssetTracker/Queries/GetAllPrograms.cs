using System;
using System.Collections.Generic;
using System.Linq;
using AssetTracker.Model;
using AssetTracker.ViewModels;
using Raven.Client;
using Raven.Client.Linq;

namespace AssetTracker.Queries
{
    public class GetAllPrograms : IRavenQuery<IEnumerable<IndividualResultViewModel>>
    {
        public void Execute(IDocumentSession session, Action<IEnumerable<IndividualResultViewModel>> reply)
        {
            reply(
                session.Query<Program>()
                    .OrderBy(x => x.Name)
                    .Select(x => new IndividualResultViewModel(x.Id, x.Name))
                    .ToList()
                );
        }
    }
}