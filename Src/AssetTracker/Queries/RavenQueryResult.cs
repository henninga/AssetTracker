using System;
using System.Threading;
using Caliburn.Micro;
using Raven.Client;

namespace AssetTracker.Queries
{
    public class RavenQueryResult<TResponse> : IResult
    {
        readonly IRavenQuery<TResponse> _ravenQuery;

        public TResponse Response { get; set; }

        public RavenQueryResult(IRavenQuery<TResponse> ravenQuery)
        {
            _ravenQuery = ravenQuery;
        }

        public void Execute(ActionExecutionContext context)
        {
            var session = IoC.Get<IDocumentSession>();

            ThreadPool.QueueUserWorkItem(state => _ravenQuery.Execute(session, response =>
            {
                Thread.Sleep(5000);
                Response = response;
                Caliburn.Micro.Execute.OnUIThread(() => Completed(this, new ResultCompletionEventArgs()));
            }));
        }


        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}