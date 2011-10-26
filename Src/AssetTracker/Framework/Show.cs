using Caliburn.Micro;

namespace AssetTracker.Framework
{
    public static class Show
    {
        public static ShowScreenResult<T> Screen<T>()
            where T : IScreen
        {
            return new ShowScreenResult<T>();
        }

        public static BusyResult Busy()
        {
            return new BusyResult(true);
        }

        public static BusyResult NotBusy()
        {
            return new BusyResult(false);
        }
    }
}