using System.Collections.Generic;
using AssetTracker.Commands;
using AssetTracker.Extensions;
using AssetTracker.Framework;
using AssetTracker.Messages;
using Caliburn.Micro;

namespace AssetTracker.ViewModels
{
    public class AddVersionViewModel : Screen
    {

        public AddVersionViewModel()
        {
            DisplayName = "Add new version";
        }

        public string Input { get; set; }

        public void Ok()
        {
            TryClose(true);
        }

        public bool CanOk
        {
            get { return !string.IsNullOrWhiteSpace(Input); }
        }

        public void Cancel()
        {
            Input = null;
            TryClose(false);
        }
    }
}