using System;
using System.Collections.Generic;
using System.Text;

namespace VhdGamer.LegacyGui
{
    public static class Options
    {
        public const string vhdlocalpath = "vhds";
        //public const string vhdserverpath = @"\\games.noproblan.ch\storage\gamepack\vhds"; 
        public const string vhdserverpath = @"\\games.lan.npl.ch\storage\vhds";
        public const string starterfilename = "startpath.txt";
        public static string nickname = "sadi";
        public const int ANNOUNCING_INTERVAL = 10000;    // we announce our games with this period
        public const int EXPIRING_INTERVAL = 20000;      // users without announcement expire after this
        public const int EXPIRING_INTERVAL_CHECK = 1000; // check every second for expiring users
    }
}
