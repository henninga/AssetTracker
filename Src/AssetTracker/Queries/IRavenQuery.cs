using System;
using Raven.Client;

namespace AssetTracker.Queries
{
    public interface IRavenQuery<out T>
    {
        void Execute(IDocumentSession session, Action<T> reply);
    }
}