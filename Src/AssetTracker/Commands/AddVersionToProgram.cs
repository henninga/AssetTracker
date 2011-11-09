using System;
using System.Collections.Generic;
using AssetTracker.Model;
using Raven.Client;

namespace AssetTracker.Commands
{
    public class AddVersionToProgram : ICommand
    {
        readonly string _programId;
        readonly string _version;

        public AddVersionToProgram(string programId, string version)
        {
            _programId = programId;
            _version = version;
        }

        public void Execute(IDocumentSession session, Action reply)
        {
            var program = session.Load<Program>(_programId);
            if (program.Versions == null)
                program.Versions = new List<ProgramVersion>();
            program.Versions.Add(new ProgramVersion
                                 {
                                     Version = _version
                                 });
            reply();
        }
    }
}