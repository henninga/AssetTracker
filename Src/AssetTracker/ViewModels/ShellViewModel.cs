using System.Collections.Generic;
using System.Linq;
using AssetTracker.DirectoryServices;
using AssetTracker.Framework;
using AssetTracker.Messages;
using AssetTracker.Queries;
using Caliburn.Micro;

namespace AssetTracker.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>, IShell
    {
        readonly IScreen _startViewModel;

        public ShellViewModel(HomeViewModel startViewModel)
        {
            _startViewModel = startViewModel;

            DisplayName = "Asset Tracker";
        }
        
        public bool IsBusy { get; set; }
        
        public IEnumerable<IResult> Home()
        {
            yield return Show.Busy();
            yield return Show.Screen<HomeViewModel>();
            yield return Show.NotBusy();
        }

        protected override void OnActivate()
        {
            ActivateItem(_startViewModel);
        }
    }
}