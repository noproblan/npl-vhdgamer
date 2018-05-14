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
    public partial class deleterForm : Form
    {
        public deleterForm()
        {
            InitializeComponent();
        }

        private void closeDownloaderButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            gameList.Enabled = false;
            foreach (FileInfo filocal in gameList.CheckedItems)
            {
                string localfilename = Application.StartupPath + @"\" + Options.vhdlocalpath + @"\" + filocal.Name;
                if (File.Exists(localfilename))
                {
                    FileSystem.DeleteFile(localfilename);
                }
            }
            updateGameList();
            gameList.Enabled = true;
        }

        private void deleterForm_Load(object sender, EventArgs e)
        {
            updateGameList();
        }

        private void updateGameList()
        {
            gameList.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            gameList.Sorted = false;
            gameList.Items.Clear();

            DirectoryInfo localdir = new DirectoryInfo(Application.StartupPath + @"\" + Options.vhdlocalpath);
            FileInfo[] finfos = localdir.GetFiles("*.vhd");
            foreach (FileInfo filocal in localdir.GetFiles("*.vhd"))
            {
                gameList.Items.Add(filocal);
            }

            gameList.Sorted = true;
            Cursor.Current = Cursors.Default;
            gameList.Enabled = true;
        }

    }
}
