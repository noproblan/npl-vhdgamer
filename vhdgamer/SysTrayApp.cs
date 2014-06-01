using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using VhdGamer.Gaming;
using VhdGamer.Communication;
using System.Collections.Generic;

namespace VhdGamer.LegacyGui
{
    public class SysTrayApp : Form
    {
        private GamesLibrary gamesLibrary;
        private Lobby lobby;
        private Timer announcingTimer;

        public static NotifyIcon trayIcon;
        private ContextMenu trayMenu;

        public SysTrayApp()
        {
            // create vhdpath directory, if it doesnt exist
            String localVhdBasePath = Path.Combine(Application.StartupPath, Options.vhdlocalpath);
            if (!Directory.Exists(localVhdBasePath))
            {
                Directory.CreateDirectory(localVhdBasePath);
            }
            gamesLibrary = new GamesLibrary(new VhdStorage(new DirectoryInfo(localVhdBasePath)));

            lobby = new Lobby();
            lobby.Listen();

            /******************************************************************************************
             * Announce Own Games every 10s
             *****************************************************************************************/
            this.announcingTimer = new Timer();
            this.announcingTimer.Interval = Options.ANNOUNCING_INTERVAL;
            this.announcingTimer.Tick += delegate(object o, EventArgs e) {
                lobby.Send(new AnnouncementMessage(new UserInfo(Options.nickname), gamesLibrary.GetGameNames()));
            };
            this.announcingTimer.Start();

            /******************************************************************************************
             * GUI Init
             *****************************************************************************************/
            trayIcon = new NotifyIcon();
            trayIcon.Text = "vhdgamer";
            trayIcon.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            // Add menu to tray icon and show it.
            trayMenu = new ContextMenu();
            trayMenu.Popup += delegate { updateContextMenu(); };

            trayIcon.ContextMenu = trayMenu;
            trayIcon.MouseClick += new MouseEventHandler(trayIcon_Click);
            trayIcon.Visible = true;

            updateContextMenu();

            // info for user (if click on tooltop -> show menu)
            trayIcon.ShowBalloonTip(1000, "vhdgamer", "Click here to start or download games...", ToolTipIcon.Info);
            trayIcon.BalloonTipClicked += delegate { trayIcon.GetType().InvokeMember("ShowContextMenu", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, trayIcon, null); };
        }

        public void updateContextMenu()
        {
            trayMenu.MenuItems.Clear();

            // get all games and add them to the context menu
            foreach (String gameName in gamesLibrary.GetGameNames())
            {
                trayMenu.MenuItems.Add(Path.GetFileNameWithoutExtension(gameName), trayMenu_Click); // remove vhd extension
            };

            // Downloader
            trayMenu.MenuItems.Add("-");
            trayMenu.MenuItems.Add("Downloader...", OnShowDownloader);

            // Deleter
            trayMenu.MenuItems.Add("-");
            trayMenu.MenuItems.Add("Clean Up...", OnShowDeleter);

            // Lobby
            trayMenu.MenuItems.Add("-");
            trayMenu.MenuItems.Add("Chat (EXPERIMENTAL)...", OnShowLobby);

            // Exit button
            trayMenu.MenuItems.Add("-");
            trayMenu.MenuItems.Add("Exit", OnExit);
        }

        // handles a click on a game entry (then mounts and starts)
        private void trayMenu_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            gamesLibrary.Play((sender as MenuItem).Text + @".vhd");
            trayIcon.ShowBalloonTip(1000, "vhdgamer", "Starting \"" + (sender as MenuItem).Text + "\"...", ToolTipIcon.Info);
            Cursor.Current = Cursors.Default;
        }

        // hide form and task bar entry
        protected override void OnLoad(EventArgs e)
        {
            Visible = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.
            base.OnLoad(e);
        }

        // handles click on tray icon
        private void trayIcon_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                trayIcon.GetType().InvokeMember("ShowContextMenu", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, trayIcon, null);
            }
        }

        private void SysTrayApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void OnShowDownloader(object sender, EventArgs e)
        {
            DownloaderForm modalForm = new DownloaderForm();
            modalForm.ShowDialog(this);
        }

        private void OnShowDeleter(object sender, EventArgs e)
        {
            DeleterForm modalForm = new DeleterForm();
            modalForm.ShowDialog(this);
        }

        private void OnShowLobby(object sender, EventArgs e)
        {
            LobbyForm modalForm = new LobbyForm(lobby);
            modalForm.ShowDialog(this);
        }

        private void OnExit(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing) { trayIcon.Dispose(); }
            base.Dispose(isDisposing);
        }
    }
}
