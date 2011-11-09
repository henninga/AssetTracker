using System;
using System.ComponentModel;


namespace AssetTracker.Settings
{
    public interface ISettingsBinder
    {
        object BindObject(Type settingsType, IBindingContext bindingContext);
    }

    public class SettingsBinder : ISettingsBinder
    {
        readonly TypeConverter _typeConverter;

        public SettingsBinder()
        {
            _typeConverter = new TypeConverter();
        }

        public object BindObject(Type settingsType, IBindingContext bindingContext)
        {
            var settingsInstance = Activator.CreateInstance(settingsType);
            var propertyInfos = settingsType.GetProperties();
            propertyInfos.Each(property =>
            {
                var value = bindingContext.ValueFor(bindingContext.Prefix + property.Name);
                if (value == null)
                    return;

                var convertedValue = _typeConverter.ConvertTo(value, property.PropertyType);


                property.SetValue(settingsInstance, convertedValue, null);
            });

            return settingsInstance;
        }
    }
}