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
        public static QueryResult<TResponse> AsResult<TResponse>(this IQuery<TResponse> query)
        {
            return new QueryResult<TResponse>(query);
        }

        public static CommandResult AsResult(this ICommand command)
        {
            return new CommandResult(command);
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