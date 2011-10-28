using System;
using AssetTracker.Model;
using Raven.Client;

namespace AssetTracker.Commands
{
    public class AddProgram : ICommand
    {
        readonly string _name;
        readonly string _notes;
        public string GeneratedId { get; private set; }

        public AddProgram(string name, string notes)
        {
            _name = name;
            _notes = notes;
        }

        public void Execute(IDocumentSession session, Action reply)
        {
            var program = new Program(_name, _notes);
            session.Store(program);
            GeneratedId = program.Id;
            reply();
        }
    }
}