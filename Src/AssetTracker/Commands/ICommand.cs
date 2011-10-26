using System;
using Raven.Client;

namespace AssetTracker.Commands
{
    public interface ICommand
    {
        void Execute(IDocumentSession session, Action reply);
    }
}