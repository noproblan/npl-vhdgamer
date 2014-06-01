using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VhdGamer.Gaming;

namespace VhdGamer.LegacyGui
{
    public partial class DeleterForm : Form
    {
        private VhdStorage localStorage;

        public DeleterForm()
        {
            localStorage = new VhdStorage(new DirectoryInfo(Options.vhdlocalpath));
            InitializeComponent();
        }

        private void DeleterForm_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            // Check Local Storage
            if (!localStorage.Location.Exists)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Lokales Pfad \"" + localStorage.Location.FullName + "\" nicht erreichbar.");
                return;
            }

            // Do the work
            updateGamelist();
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Update the ListView containing serverside Vhd names. Local Vhds are checked.
        /// </summary>
        private void updateGamelist()
        {
            gameList.Enabled = false;
            gameList.Sorted = false;
            gameList.Items.Clear();

            ICollection<String> localVhdNames = localStorage.GetVhdNames();

            // Add local Vhds to list
            foreach (String s in localVhdNames)
            {
                gameList.Items.Add(s);
            }

            gameList.Sorted = true;
            gameList.Enabled = true;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            string basepath = localStorage.Location.FullName;
            foreach (String vhdname in gameList.CheckedItems)
            {
                File.Delete(Path.Combine(basepath, vhdname));
            }
            updateGamelist();
        }

        private void cancelDeletionButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
