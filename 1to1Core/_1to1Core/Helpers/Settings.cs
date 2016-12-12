using System;
using System.Collections.Generic;
using System.Text;

namespace _1to1Core
{
    public static class Settings
    {
        // HockeyApp App IDs, we have one for each supported platform
#if WINDOWS_UWP
        public static string HockeyAppId = "1c52884e1fdf4977ba1e2ca004c0b59c";
#elif __ANDROID__
        public static string HockeyAppId = "e1eaada60c094131bbefd332da4bfb4d";
#elif __IOS__
        public static string HockeyAppId = "786ecbec0b564d03974dc94c96f50fb0";
#endif

    }
}
