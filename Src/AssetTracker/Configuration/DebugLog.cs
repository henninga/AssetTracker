using System;
using System.Diagnostics;
using Caliburn.Micro;

namespace AssetTracker.Configuration
{
    public class DebugLog : ILog
    {
        public void Info(string format, params object[] args)
        {
            Debug.WriteLine(format, args);
        }

        public void Warn(string format, params object[] args)
        {
            Debug.WriteLine(format, args);
        }

        public void Error(Exception exception)
        {
            Debug.WriteLine(exception);
        }
    }
}