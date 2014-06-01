using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VhdGamer.Communication
{
    [Serializable]
    public abstract class Message
    {
        public readonly DateTime Timestamp;
        public UserInfo User;

        protected Message()
        {
            this.Timestamp = DateTime.Now;
        }
    }

    [Serializable]
    public class ChatMessage : Message
    {
        public String Text;
        public String Channel;

        public ChatMessage(String text)
        {
            this.User = new UserInfo("anonymous");
            this.Text = text;
        }

        public ChatMessage(UserInfo user, String text)
        {
            this.User = user;
            this.Text = text;
        }
    }
}
