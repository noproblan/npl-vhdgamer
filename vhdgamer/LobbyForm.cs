using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VhdGamer.Communication;
using VhdGamer.Gaming;

namespace VhdGamer.LegacyGui
{
    public partial class LobbyForm : Form
    {
        private Lobby lobby;
        private UserInfo currentUser;
        private Dictionary<UserInfo, long> userTimeouts; // for removing expired users
        private Dictionary<UserInfo, ICollection<String>> userGames;
        private ChatTabPage tabPageAll;
        private Timer expiringTimer;

        public LobbyForm(Lobby lobby)
        {
            InitializeComponent();

            this.lobby = lobby;
            this.userGames = new Dictionary<UserInfo, ICollection<String>>();
            this.nicknameTextBox.Text = Options.nickname;
            this.currentUser = new UserInfo(Options.nickname);
            this.lobby.OnMessageReveived += lobby_OnMessageReveived;

            // GUI Config
            tabPageAll = new ChatTabPage("Broadcast");
            tabPageAll.Text = "All:";
            this.tabControlChat.TabPages.Add(tabPageAll);

            this.messageTextBox.KeyDown += new KeyEventHandler(delegate(object o, KeyEventArgs e) {
                if (e.KeyCode == Keys.Enter)
                {
                    sendMessageButton.PerformClick();
                }
            });

            this.gamesListBox.ContextMenu = new ContextMenu();
            this.gamesListBox.ContextMenu.MenuItems.Add(0, new MenuItem("Join Game Channel", delegate(object o, EventArgs e)
            {
                ChatTabPage t = joinChannel(this.gamesListBox.SelectedItem.ToString());
                t.Select();
            }));

            /******************************************************************************************
             * Check every second for expiring users
             *****************************************************************************************/
            /*
            this.expiringTimer = new Timer();
            this.expiringTimer.Interval = Options.EXPIRING_INTERVAL_CHECK;
            this.expiringTimer.Tick += delegate(object o, EventArgs e)
            {
                userTimeouts.
                foreach(KeyValuePair<UserInfo, long> k in userTimeouts) 
                {
                    if (k.Value > CurrentMillis.Millis)
                    {
                        userTimeouts.
                    }
                }
            };
            this.expiringTimer.Start();
             */
        }

        private void LobbyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            lobby.OnMessageReveived -= lobby_OnMessageReveived;
        }

        private void lobby_OnMessageReveived(object sender, MessageEventArgs e)
        {
            VhdGamer.Communication.Message m = e.Message;
            Debug.WriteLine("Got Message of Type: "+m);

            // We got a chat message
            if (m.GetType() == typeof(ChatMessage))
            {
                ChatMessage c = (ChatMessage)m;
                processChatMessage(c);
            }

            // We just got an announcement
            if (m.GetType() == typeof(AnnouncementMessage))
            {
                AnnouncementMessage a = (AnnouncementMessage)m;
                processAnnouncementMessage(a);
            }
        }

        private void processAnnouncementMessage(AnnouncementMessage msg)
        {
            userGames.Add(msg.User, msg.GameNames);
            // check if the user is known already
            usersComboBox.Invoke((MethodInvoker)delegate()
            {
                if (usersComboBox.FindString(msg.User.Name) == ListBox.NoMatches)
                {
                    int i = usersComboBox.Items.Add(msg.User);
                    // if it is us, we select our entry
                    if (msg.User.Name == currentUser.Name && i >= 0)
                    {
                        this.usersComboBox.SelectedIndex = i;
                    }
                }
                updateChannelUsers(tabPageAll.Channel, msg.User);
            });
        }

        private void processChatMessage(ChatMessage msg)
        {
            if (msg.Channel == null)
            {
                updateChannelUsers(tabPageAll.Channel, msg.User);
                tabPageAll.ChatField.Invoke((MethodInvoker)delegate()
                {
                    tabPageAll.AppendMessage(msg);
                });
            }
            else
            {
                ChatTabPage t = joinChannel(msg.Channel);
                updateChannelUsers(msg.Channel, msg.User);
                t.AppendMessage(msg);
            }
        }

        private void updateChannelUsers(string chanelNo5, UserInfo userInfo)
        {
            // Search Channel in all Tabs (there's no index for the channels)
            foreach (ChatTabPage t in tabControlChat.TabPages)
            {
                if (t.Channel == chanelNo5) // Ha! We found it!
                {
                    // search for existing username in channel
                    bool userInChannel = false;
                    foreach (UserInfo i in t.Users)
                    {
                        if (i.Name == userInfo.Name)
                        {
                            userInChannel = true;
                            break;
                        }
                    }

                    // If the user is NOT listed already, we add him
                    if (!userInChannel)
                    {
                        t.Users.Add(userInfo);
                        tabControlChat.Invoke(new MethodInvoker(delegate() {
                            if (t == tabControlChat.SelectedTab)
                            {
                                channelUsersListBox.Items.Add(userInfo);
                            }
                        }));
                        break;
                    }
                }
            }
        }

        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            // send message to users of a selected tab (group chat)
            ChatTabPage t = (ChatTabPage)tabControlChat.SelectedTab;
            if (t == tabPageAll)
            {
                sendBroadcastMessage(messageTextBox.Text);
            }
            else
            {
                sendChannelMessage(t.Channel, messageTextBox.Text);
            }
        }

        private void saveNicknameButton_Click(object sender, EventArgs e)
        {
            Options.nickname = nicknameTextBox.Text;
            currentUser.Name = Options.nickname;
        }

        private void usersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void sendBroadcastMessage(String text)
        {
            ChatMessage message = new ChatMessage(currentUser, text);
            lobby.Send(message);
            messageTextBox.Text = "";
            messageTextBox.Focus();
        }

        private void sendChannelMessage(String channel, string text)
        {
            ChatMessage message = new ChatMessage(currentUser, text);
            message.Channel = channel;
            lobby.Send(message);
            messageTextBox.Text = "";
            messageTextBox.Focus();
        }

        private ChatTabPage joinChannel(string channel)
        {
            ChatTabPage chatTabPage = null;
            // get existing tab page
            foreach(TabPage t in tabControlChat.TabPages)
            {
                if (((ChatTabPage)t).Channel == channel)
                {
                    chatTabPage = (ChatTabPage)t;
                    break;
                }
            }
            // or create a new one
            if (chatTabPage == null)
            {
                chatTabPage = new ChatTabPage(channel);
                this.tabControlChat.TabPages.Add(chatTabPage);
            }
            return chatTabPage;
        }

        private void usersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserInfo info = (UserInfo)this.usersComboBox.SelectedItem;
            gamesListBox.Items.Clear();
            foreach(String gameName in userGames[info])
            {
                gamesListBox.Items.Add(gameName);
            }
        }

        private void tabControlChat_SelectedIndexChanged(object sender, EventArgs e)
        {
            channelUsersListBox.Items.Clear();
            foreach (UserInfo i in ((ChatTabPage)this.tabControlChat.SelectedTab).Users)
            {
                channelUsersListBox.Items.Add(i);
            }
        }

        private void LobbyForm_Load(object sender, EventArgs e)
        {

        }
    }
}
