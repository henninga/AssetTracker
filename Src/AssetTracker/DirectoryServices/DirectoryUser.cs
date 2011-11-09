using System.Diagnostics;

namespace AssetTracker.DirectoryServices
{
    [DebuggerDisplay("{FullName}, {Username}")]
    public class DirectoryUser
    {
        public string Username { get; set; }
        public string FullName { get; set; }

        public DirectoryUser(){}

        public DirectoryUser(string username, string fullName)
        {
            Username = username;
            FullName = fullName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (DirectoryUser)) return false;
            return Equals((DirectoryUser) obj);
        }

        public bool Equals(DirectoryUser other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Username, Username) && Equals(other.FullName, FullName);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Username != null ? Username.GetHashCode() : 0)*397) ^ (FullName != null ? FullName.GetHashCode() : 0);
            }
        }
    }
}