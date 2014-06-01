using System;
using System.Diagnostics;
using System.IO;

namespace VhdGamer.Gaming
{
    public class VhdGame
    {
        public const String STARTERFILE_PATH = "startpath.txt";
        private readonly Vhd vhd;

        public VhdGame(Vhd vhd)
        {
            this.vhd = vhd;
        }

        public void Play()
        {
            if (!vhd.IsMounted)
            {

                vhd.Mount();
            }

            // Retrieve Path from startpath file
            String basePath = vhd.GetMountLocation().FullName;
            String starterfilePath = Path.Combine(basePath, STARTERFILE_PATH);
            TextReader tr = new StreamReader(starterfilePath);
            String exePath = Path.Combine(basePath, tr.ReadLine());
            tr.Close();

            // Start executable and await its ending
            ProcessStartInfo startInfo = new ProcessStartInfo(exePath);
            startInfo.WorkingDirectory = Path.GetDirectoryName(exePath);
            Process P = Process.Start(startInfo);
            IntPtr runningGameHandle = P.Handle;
            P.WaitForExit();
        }
    }
}
