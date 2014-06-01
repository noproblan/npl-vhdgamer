using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using VhdGamer.LegacyGui;

namespace VhdGamer
{
    static class Program
    {
        // for checking if application already started
        private static Mutex AppMutex = new Mutex(false, "MainApp");

        [STAThread]
        static void Main()
        {
            if (AppMutex.WaitOne(0, false))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new SysTrayApp());
            }
            else
            {
                MessageBox.Show("Vhdgamer is already running. Check your SysTray for a black dice symbol.");
            }
            Application.Exit();
        }
    }
}
