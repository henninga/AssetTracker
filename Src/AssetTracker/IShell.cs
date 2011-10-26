using Caliburn.Micro;

namespace AssetTracker {
    public interface IShell : IConductor
    {
        bool IsBusy { get; set; }
    }
}
