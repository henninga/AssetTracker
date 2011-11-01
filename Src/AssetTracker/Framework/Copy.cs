using System;

namespace AssetTracker.Framework
{
    public static class Copy
    {
        public static Action<string> CopyAction = source => { };

        public static void ToClipboard(this string source)
        {
            CopyAction(source);
        }
    }
}