using System;

namespace AssetTracker.Settings
{
    public interface ISettingsProvider
    {
        T SettingsFor<T>();
        object SettingsFor(Type settingsType);
    }

    public class AppSettingsProvider : ISettingsProvider
    {
        readonly IRequestData _requestData;
        readonly ISettingsBinder _binder;

        public AppSettingsProvider(IRequestData requestData, ISettingsBinder binder)
        {
            _requestData = requestData;
            _binder = binder;
        }

        public T SettingsFor<T>()
        {
            var settings = SettingsFor(typeof (T));
            return (T) settings;
        }

        public object SettingsFor(Type settingsType)
        {
            var bindingContext = new BindingContext(_requestData).PrefixWith(settingsType.Name + ".");
            var result = _binder.BindObject(settingsType, bindingContext);
            return result;
        }
    }
}