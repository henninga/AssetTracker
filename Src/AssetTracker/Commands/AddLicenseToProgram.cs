using System;
using System.Linq;
using AssetTracker.Model;
using Raven.Client;

namespace AssetTracker.Commands
{
    public class AddLicenseToProgram : ICommand
    {
        readonly string _programId;
        readonly string _version;
        readonly string _username;
        readonly string _key;

        public AddLicenseToProgram(string programId, string version, string username, string key)
        {
            _programId = programId;
            _version = version;
            _username = username;
            _key = key;
        }

        public void Execute(IDocumentSession session, Action reply)
        {
            var program = session.Load<Program>(_programId);
            var version = program.Versions.Single(x => x.Version == _version);
            version.Licenses.Add(new VersionLicense
                                 {
                                     Username = _username,
                                     Key = _key
                                 });
            reply();
        }
    }
}