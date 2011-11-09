using System;
using Caliburn.Micro;

namespace AssetTracker.Framework
{
    public class BusyResult : IResult
    {
        readonly bool _isBusy;

        public BusyResult(bool isBusy)
        {
            _isBusy = isBusy;
        }

        public void Execute(ActionExecutionContext context)
        {
            var shell = IoC.Get<IShell>();
            shell.IsBusy = _isBusy;

            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}