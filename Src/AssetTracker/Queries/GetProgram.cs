using System;
using AssetTracker.Model;
using Raven.Client;

namespace AssetTracker.Queries
{
    public class GetProgram : IRavenQuery<Program>
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