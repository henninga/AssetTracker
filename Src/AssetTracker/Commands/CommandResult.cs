using System;
using System.Threading;
using Caliburn.Micro;
using Raven.Client;

namespace AssetTracker.Commands
{
    public class CommandResult : IResult
    {
        readonly ICommand _command;

        public CommandResult(ICommand command)
        {
            _command = command;
        }

        public void Execute(ActionExecutionContext context)
        {
            var session = IoC.Get<IDocumentSession>();

            ThreadPool.QueueUserWorkItem(state => _command.Execute(session, () =>
            {
                session.SaveChanges();
                Caliburn.Micro.Execute.OnUIThread(() => Completed(this, new ResultCompletionEventArgs()));
            }));
        }

        public event EventHandler<ResultCompletionEventArgs> Completed;
    }
}