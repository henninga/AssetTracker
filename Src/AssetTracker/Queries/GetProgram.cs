using System;
using AssetTracker.Extensions;
using AssetTracker.Model;
using AssetTracker.ViewModels;
using Raven.Client;
using Raven.Client.Linq;

namespace AssetTracker.Queries
{
    public class GetProgram : IQuery<Program>
    {
        readonly string _id;

        public GetProgram(string id)
        {
            _id = id;
        }

        public void Execute(IDocumentSession session, Action<Program> reply)
        {
            reply(
                    session.Load<Program>(_id)
                );
        }
    }
}