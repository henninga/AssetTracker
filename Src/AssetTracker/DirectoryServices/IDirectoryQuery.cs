using System;

namespace AssetTracker.DirectoryServices
{
    public interface IDirectoryQuery<out T>
    {
        void Execute(Action<T> reply);
    }
}