using System;
using System.Threading;
using Caliburn.Micro;
using Raven.Client;

namespace AssetTracker.Queries
{
    public class QueryResult<TResponse> : IResult
    {
        readonly IQuery<TResponse> _query;

        public TResponse Response { get; set; }

        public QueryResult(IQuery<TResponse> query)
        {
            _query = query;
        }

        public void Execute(ActionExecutionContext context)
        {
            var session = IoC.Get<IDocumentSession>();

            ThreadPool.QueueUserWorkItem(state => _query.Execute(session, response =>
            {
                Response = response;
                Caliburn.Micro.Execute.OnUIThread(() => Completed(this, new ResultCompletionEventArgs()));
            }));
        }


        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}