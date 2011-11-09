using System;
using System.Collections.Generic;
using AssetTracker.Commands;
using AssetTracker.DirectoryServices;
using AssetTracker.Queries;

namespace AssetTracker
{
    public static class Extensions
    {
        public static RavenQueryResult<TResponse> AsQuery<TResponse>(this IRavenQuery<TResponse> ravenQuery)
        {
            return new RavenQueryResult<TResponse>(ravenQuery);
        }

        public static DirectoryQueryResult<TResponse> AsQuery<TResponse>(this IDirectoryQuery<TResponse> query)
        {
            return new DirectoryQueryResult<TResponse>(query);
        }

        public static CommandResult<T> AsCommand<T>(this T command) where T : ICommand
        {
            return new CommandResult<T>(command);
        }

        public static void Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}