using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VhdGamer.Communication;

namespace VhdGamer
{
    class ChatTabPage : TabPage
    {
        public RichTextBox ChatField;
        public String Channel;
        public ICollection<UserInfo> Users;

        public ChatTabPage(String channelName)
        {
            Users = new List<UserInfo>();
            Channel = channelName;
            Text = channelName;

            ChatField = new RichTextBox();
            ChatField.Dock = DockStyle.Fill;
            ChatField.Text = "";

            Controls.Add(this.ChatField);
        }

        public void AppendMessage(ChatMessage c)
        {
            ChatField.Invoke(new MethodInvoker(delegate()
            {
                ChatField.AppendText(String.Format("{0} - {1}: {2}{3}", c.Timestamp.ToShortTimeString(), c.User.Name, c.Text, Environment.NewLine));
            }));
        }
    }
}
