using AssetTracker.DirectoryServices;
using AssetTracker.Settings;
using AssetTracker.ViewModels;
using Caliburn.Micro;
using Raven.Client;
using Raven.Client.Document;
using StructureMap.Configuration.DSL;

namespace AssetTracker.Configuration
{
    public class AssetTrackerRegistry : Registry
    {
        public AssetTrackerRegistry()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.AssemblyContainingType<IEventAggregator>();

                scanner.WithDefaultConventions();
            });

            For<IDocumentStore>().Use(x => new DocumentStore
                                           {
                                               Url = "http://localhost:8080"
                                           }.Initialize());
            For<IDocumentSession>().Use(x => x.GetInstance<IDocumentStore>().OpenSession());

            For<IEventAggregator>().Singleton().Use<EventAggregator>();
            For<IShell>().Singleton().Use<ShellViewModel>();
            For<IRequestData>().Use<AppSettingsRequestData>();
            For<ISettingsProvider>().Use<AppSettingsProvider>();
            For<DirectorySettings>().Use(x => x.GetInstance<ISettingsProvider>().SettingsFor<DirectorySettings>());
        }
    }
}