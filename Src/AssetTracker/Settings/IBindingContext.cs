namespace AssetTracker.Settings
{
    public interface IBindingContext
    {
        string Prefix { get; }
        //bool IncludeValuesWithoutPrefix { get; }
        //bool CaseSensitive { get; }

        object ValueFor(string key);
    }
}