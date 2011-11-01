using System;
using System.Linq;
using AssetTracker.Model;
using Raven.Client;

namespace AssetTracker.Commands
{
    public class DeleteVersion : ICommand
    {
        readonly string _id;
        readonly string _version;

        public DeleteVersion(string id, string version)
        {
            _id = id;
            _version = version;
        }

        public void Execute(IDocumentSession session, Action reply)
        {
            var program = session.Load<Program>(_id);
            var version = program.Versions.SingleOrDefault(x => x.Version == _version);
            if (version != null)
                program.Versions.Remove(version);
            reply();
        }
    }
}