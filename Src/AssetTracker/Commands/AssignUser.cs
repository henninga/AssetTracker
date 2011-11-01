using System;
using System.Linq;
using AssetTracker.Model;
using Raven.Client;

namespace AssetTracker.Commands
{
    public class AssignUser : ICommand
    {
        readonly string _programId;
        readonly string _version;
        readonly string _licenseUsername;
        readonly string _licenseKey;
        readonly string _currentUsername;


        public AssignUser(string programId, string version, string licenseUsername, string licenseKey, string currentUsername)
        {
            _programId = programId;
            _version = version;
            _licenseUsername = licenseUsername;
            _licenseKey = licenseKey;
            _currentUsername = currentUsername;
        }

        public void Execute(IDocumentSession session, Action reply)
        {
            var program = session.Load<Program>(_programId);
            var version = program.Versions.SingleOrDefault(x => x.Version == _version);
            if (version != null)
            {
                var license = version.Licenses.SingleOrDefault(x => x.Key == _licenseKey && x.Username == _licenseUsername);
                if (license != null)
                    license.AssignedToUser = _currentUsername;
            }

            reply();
        }
    }
}