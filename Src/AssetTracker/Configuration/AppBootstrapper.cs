using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using AssetTracker.Framework;
using AssetTracker.UserControls;
using Caliburn.Micro;
using StructureMap;

//using StructureMap;

namespace AssetTracker.Configuration
{
    public class AppBootstrapper : Bootstrapper<IShell>
    {
        IContainer _container;

        protected override void Configure()
        {
            _container = new Container(new AssetTrackerRegistry());
            ConventionManager.AddElementConvention<BusyIndicator>(BusyIndicator.IsBusyProperty, "IsBusy", null);
            ConventionManager.AddElementConvention<MenuItem>(MenuItem.CommandProperty, "Command", null);
            LogManager.GetLog = type => new DebugLog();
            Copy.CopyAction = source => { Clipboard.SetText(source); };

            Show.Confirmation = (text, header) =>
            {
                var result = MessageBox.Show(text, header, MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes || result == MessageBoxResult.OK)
                    return true;

                return false;
            };
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            //var currentUser = WindowsIdentity.GetCurrent();
            //if (currentUser == null || !currentUser.IsAuthenticated || currentUser.IsAnonymous)
            //    return;

            //Thread.CurrentPrincipal = new WindowsPrincipal(currentUser);
            //AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            base.OnStartup(sender, e);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            return string.IsNullOrEmpty(key)
                       ? _container.GetInstance(serviceType)
                       : _container.GetInstance(serviceType ?? typeof (object), key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}