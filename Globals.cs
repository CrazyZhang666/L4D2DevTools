namespace L4D2DevTools
{
    public static class Globals
    {
        public static string L4D2MainExec { get; set; } = string.Empty;
        public static string L4D2MainDir { get; set; } = string.Empty;

        public static string VPKExec
        {
            get { return $"{L4D2MainDir}\\bin\\vpk.exe"; }
        }

        public static string HammerExec
        {
            get { return $"{L4D2MainDir}\\bin\\hammer.exe"; }
        }

        public static string L4D2AddonsDir
        {
            get { return $"{L4D2MainDir}\\left4dead2\\addons"; }
        }

        public static string L4D2MapsDir
        {
            get { return $"{L4D2MainDir}\\left4dead2\\maps"; }
        }
    }
}
