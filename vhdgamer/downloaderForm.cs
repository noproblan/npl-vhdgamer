using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace vhdgamer
{
    public partial class downloaderForm : Form
    {
        public downloaderForm()
        {
            InitializeComponent();
        }

        private void downloaderForm_Load(object sender, EventArgs e)
        {
            updateGamelist();
        }

        private void closeDownloaderButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void syncButton_Click(object sender, EventArgs e)
        {
            gameList.Enabled = false;
            foreach (FileInfo fiserver in gameList.CheckedItems)
            {
                string localfilename = Application.StartupPath + @"\" + Options.vhdlocalpath + @"\" + fiserver.Name;
                if (!File.Exists(localfilename))
                {
                    //fiserver.CopyTo(localfilename); // has no progess bar
                    try
                    {
                        FileSystem.CopyFile(fiserver.FullName, localfilename, UIOption.AllDialogs);
                    }
                    catch (System.OperationCanceledException)
                    {
                        updateGamelist();
                        gameList.Enabled = true;
                        return;
                    }
                }
            }

            // notification indicating finished download
            SysTrayApp.trayIcon.ShowBalloonTip(1000, "vhdgamer", "Downloads finished.", ToolTipIcon.Info);
            gameList.Enabled = true;
        }

        private void updateGamelist()
        {
            gameList.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            gameList.Sorted = false;
            gameList.Items.Clear();

            DirectoryInfo localdir = new DirectoryInfo(Application.StartupPath + @"\" + Options.vhdlocalpath);
            DirectoryInfo serverdir = new DirectoryInfo(Options.vhdserverpath);

            // if the server directory doesn't exists or isn't reachable
            SysTrayApp.trayIcon.ShowBalloonTip(20, "vhdgamer", "Connecting to Server...", ToolTipIcon.Info);
            if (!serverdir.Exists)
            {
                MessageBox.Show("Serverpfad \"" + serverdir.FullName + "\" nicht erreichbar.");
                Close();
                this.Cursor = Cursors.Default;
                return;
            }

            FileInfo[] finfos = localdir.GetFiles("*.vhd");
            foreach (FileInfo fiserver in serverdir.GetFiles("*.vhd"))
            {
                gameList.Items.Add(fiserver);
                foreach (FileInfo ficlient in finfos)
                {
                    // check if a vhd is already on the users hd (if yes, check it)
                    if (ficlient.Name == fiserver.Name)
                    {
                        // mark last inserted element
                        gameList.SetItemChecked(gameList.Items.Count - 1, true);
                    }
                }
            }

            gameList.Sorted = true;
            Cursor.Current = Cursors.Default;
            gameList.Enabled = true;
        }
    }
}
