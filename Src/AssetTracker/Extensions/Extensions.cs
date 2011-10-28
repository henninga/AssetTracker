using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AssetTracker.Commands;
using AssetTracker.Model;
using AssetTracker.Queries;
using AssetTracker.ViewModels;

namespace AssetTracker.Extensions
{
    public static class Extensions
    {
        public static QueryResult<TResponse> AsQuery<TResponse>(this IQuery<TResponse> query)
        {
            return new QueryResult<TResponse>(query);
        }

        public static CommandResult<T> AsCommand<T>(this T command)where T : ICommand
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