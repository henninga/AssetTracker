using System;
using Raven.Client;

namespace AssetTracker.Queries
{
    public interface IQuery<out T>
    {
        void Execute(IDocumentSession session, Action<T> reply);
    }
}