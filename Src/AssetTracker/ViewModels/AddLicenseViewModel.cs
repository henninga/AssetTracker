using System.Collections.Generic;
using AssetTracker.Commands;
using AssetTracker.Extensions;
using AssetTracker.Framework;
using AssetTracker.Messages;
using Caliburn.Micro;

namespace AssetTracker.ViewModels
{
    public class AddLicenseViewModel : Screen
    {

        public AddLicenseViewModel()
        {
            DisplayName = "Add new license";
        }

        public string Username { get; set; }
        public string Key { get; set; }

        public void Ok()
        {
            TryClose(true);
        }

        public bool CanOk
        {
            get { return !string.IsNullOrWhiteSpace(Key); }
        }

        public void Cancel()
        {
            Key = null;
            Username = null;
            TryClose(false);
        }
    }
}