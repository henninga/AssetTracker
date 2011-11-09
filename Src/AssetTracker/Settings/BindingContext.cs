namespace AssetTracker.Settings
{
    public class BindingContext : IBindingContext
    {
        readonly IRequestData _requestData;
        string _prefix;
        bool _includeValuesWithNoPrefix;
        bool _isCaseSensitive = true;

        public string Prefix
        {
            get { return _prefix; }
        }

        //public bool IncludeValuesWithoutPrefix { get { return _includeValuesWithNoPrefix; } }
        //public bool CaseSensitive { get { return _isCaseSensitive; } }


        public BindingContext(IRequestData requestData)
        {
            _requestData = requestData;
        }

        public BindingContext PrefixWith(string prefix)
        {
            _prefix = prefix;
            return this;
        }

        public BindingContext IncludeValuesWithNoPrefix()
        {
            _includeValuesWithNoPrefix = true;
            return this;
        }

        public BindingContext IsCaseSensitive(bool value)
        {
            _isCaseSensitive = value;
            return this;
        }

        public object ValueFor(string key)
        {
            return _requestData.Value(key);
        }
    }
}