using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
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
            LogManager.GetLog = type => new DebugLog();
            
        }
        
        

        protected override object GetInstance(Type serviceType, string key)
        {
            return string.IsNullOrEmpty(key)
                       ? _container.GetInstance(serviceType)
                       : _container.GetInstance(serviceType ?? typeof(object), key);
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
