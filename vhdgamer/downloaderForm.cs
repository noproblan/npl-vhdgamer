using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using VhdGamer.Gaming;

namespace VhdGamer.LegacyGui
{
    public partial class DownloaderForm : Form
    {
        private VhdStorage localStorage, serverStorage;

        public DownloaderForm()
        {
            serverStorage = new VhdStorage(new DirectoryInfo(Options.vhdserverpath));
            localStorage = new VhdStorage(new DirectoryInfo(Options.vhdlocalpath));
            InitializeComponent();
        }


        private void downloaderForm_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            // Check Local Storage
            if (!localStorage.Location.Exists)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Lokales Pfad \"" + localStorage.Location.FullName + "\" nicht erreichbar.");
                return;
            }

            // Check Server Connection
            SysTrayApp.trayIcon.ShowBalloonTip(20, "vhdgamer", "Connecting to Server...", ToolTipIcon.Info);
            if (!serverStorage.Location.Exists)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Serverpfad \"" + serverStorage.Location.FullName + "\" nicht erreichbar.");
                return;
            }

            // Do the work
            updateGamelist();
            Cursor.Current = Cursors.Default;
        }

        private void closeDownloaderButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void syncButton_Click(object sender, EventArgs e)
        {
            gameList.Enabled = false;
            
            // All checked items are to be synchronized
            ICollection<String> filter = new List<String>();
            foreach (String s in gameList.CheckedItems) 
            {
                filter.Add(s);
            }

            // Synchronize checked items from server to local
            localStorage.Syncronize(serverStorage, filter);

            // notification indicating finished download
            SysTrayApp.trayIcon.ShowBalloonTip(1000, "vhdgamer", "Downloads finished.", ToolTipIcon.Info);
            gameList.Enabled = true;
        }

        /// <summary>
        /// Update the ListView containing serverside Vhd names. Local Vhds are checked.
        /// </summary>
        private void updateGamelist()
        {
            gameList.Enabled = false;
            gameList.Sorted = false;
            gameList.Items.Clear();

            // Name lists as cache, though we don't need to retrieve it every iteration
            ICollection<String> localVhdNames = localStorage.GetVhdNames();
            ICollection<String> serverVhdNames = serverStorage.GetVhdNames();

            // Add serverside Vhds to list
            foreach (String s in serverVhdNames)
            {
                gameList.Items.Add(s);
                if (localVhdNames.Contains(s)) // check if it already exists locally
                {
                    gameList.SetItemChecked(gameList.Items.Count - 1, true);
                }
            }

            gameList.Sorted = true;
            gameList.Enabled = true;
        }

    }
}
