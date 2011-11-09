using System;
using System.Threading;
using AssetTracker.Queries;
using Caliburn.Micro;
using Raven.Client;

namespace AssetTracker.DirectoryServices
{
    public class DirectoryQueryResult<TResponse> : IResult
    {
        readonly IDirectoryQuery<TResponse> _query;

        public TResponse Response { get; set; }

        public DirectoryQueryResult(IDirectoryQuery<TResponse> query)
        {
            _query = query;
        }

        public void Execute(ActionExecutionContext context)
        {
           ThreadPool.QueueUserWorkItem(state => _query.Execute(response =>
            {
                Response = response;
                Caliburn.Micro.Execute.OnUIThread(() => Completed(this, new ResultCompletionEventArgs()));
            }));
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}