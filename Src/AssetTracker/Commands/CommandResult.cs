using System;
using System.Threading;
using Caliburn.Micro;
using Raven.Client;

namespace AssetTracker.Commands
{
    public class CommandResult<T> : IResult where T : ICommand
    {
        readonly T _command;
        public T Command { get { return _command; } }

        public CommandResult(T command)
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