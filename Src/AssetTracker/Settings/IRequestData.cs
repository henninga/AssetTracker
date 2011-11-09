using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace AssetTracker.Settings
{
    public interface IRequestData
    {
        IEnumerable<string> GetKeys();
        bool HasAnyValuePrefixedWith(string key);
        object Value(string key);
    }

    public class AppSettingsRequestData : IRequestData
    {
        public IEnumerable<string> GetKeys()
        {
            return ConfigurationManager.AppSettings.AllKeys;
        }

        public bool HasAnyValuePrefixedWith(string key)
        {
            return GetKeys().Any(x => x.StartsWith(key));
        }

        public object Value(string key)
        {
            var value = ConfigurationManager.AppSettings[key];
            return value;
        }
    }
}