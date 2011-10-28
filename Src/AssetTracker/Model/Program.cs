using System;
using System.Collections.Generic;

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
        public IList<ProgramVersion> ProgramVersions { get; set; }
    }

    public class ProgramVersion
    {
        public ProgramVersion()
        {
            Licenses = new List<ProgramVersionLicense>();
        }

        public string Version { get; set; }
        public IList<ProgramVersionLicense> Licenses { get; set; }
    }

    public class ProgramVersionLicense
    {
        public string Key { get; set; }
        public string Username { get; set; }
        public string AssignedToName { get; set; }
        public string AssignedToUser { get; set; }
    }

    public abstract class Entity
    {
        public string Id { get; set; }
    }
}