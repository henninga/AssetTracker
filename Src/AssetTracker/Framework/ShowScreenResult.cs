using System;
using Caliburn.Micro;

namespace AssetTracker.Framework
{
    public class ShowScreenResult<T> : IResult
    {
        Action<T> _configruation;

        public ShowScreenResult<T> Configured(Action<T> configuration)
        {
            _configruation = configuration;
            return this;
        }

        public void Execute(ActionExecutionContext context)
        {
            var shell = IoC.Get<IShell>();
            var screen = IoC.Get<T>();

            if (_configruation != null)
                _configruation(screen);

            shell.ActivateItem(screen);

            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}