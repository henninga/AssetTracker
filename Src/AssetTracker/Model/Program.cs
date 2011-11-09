using System.Collections.Generic;
using AssetTracker.DirectoryServices;

namespace AssetTracker.Model
{
    public class Program : Entity
    {
        public Program()
        {
        }

        public Program(string name, string notes)
        {
            Name = name;
            Notes = notes;
        }

        public string Name { get; set; }
        public string Notes { get; set; }
        public IList<ProgramVersion> Versions { get; set; }
    }

    public class ProgramVersion
    {
        public ProgramVersion()
        {
            Licenses = new List<VersionLicense>();
        }

        public string Version { get; set; }
        public IList<VersionLicense> Licenses { get; set; }
    }

    public class VersionLicense
    {
        public string Key { get; set; }
        public string Username { get; set; }
        public DirectoryUser AssignedToUser { get; set; }
    }

    public abstract class Entity
    {
        public string Id { get; set; }
    }
}