using System.Collections.Generic;

namespace AssetTracker.Model
{
    public class Program : Entity
    {
        public string Name { get; set; }
        public string Notes { get; set; }
        public IList<ProgramVersion> ProgramVersions { get; set; }
    }

    public class ProgramVersion
    {
        public string Version { get; set; }
        public IList<ProgramVersionLicenses> VersionLicenseses { get; set; }
    }

    public class ProgramVersionLicenses
    {
        public string Key { get; set; }
        public string AssignedToName { get; set; }
        public string AssignedToUser { get; set; }
    }

    public abstract class Entity
    {
        public string Id { get; set; }
    }
}