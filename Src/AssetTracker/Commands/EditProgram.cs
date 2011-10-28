using System;
using AssetTracker.Model;
using Raven.Client;

namespace AssetTracker.Commands
{
    public class EditProgram : ICommand
    {
        private readonly string _id;
        readonly string _name;
        readonly string _notes;

        public EditProgram(string id,string name, string notes)
        {
            _id = id;
            _name = name;
            _notes = notes;
        }

        public void Execute(IDocumentSession session, Action reply)
        {
            var program = session.Load<Program>(_id);
            
            program.Name = _name;
            program.Notes = _notes;
            
            reply();
        }
    }
}