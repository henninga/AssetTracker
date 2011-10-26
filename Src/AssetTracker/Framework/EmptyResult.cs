using System;
using Caliburn.Micro;

namespace AssetTracker.Framework
{
    public class EmptyResult : IResult
    {
        public void Execute(ActionExecutionContext context)
        {
            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed;
    }
}