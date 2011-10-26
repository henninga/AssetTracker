using System;
using AssetTracker.Model;
using Raven.Client;

namespace AssetTracker.Commands
{
    public class AddProgram : ICommand
    {
        readonly string _name;
        readonly string _notes;

        public AddProgram(string name, string notes)
        {
            _name = name;
            _notes = notes;
        }

        public void Execute(IDocumentSession session, Action reply)
        {
            session.Store(new Program()
                          {
                              Name = _name,
                              Notes = _notes
                          });
            reply();
        }
    }
}