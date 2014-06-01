using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VhdGamer.Communication;

namespace VhdGamer.LegacyGui
{
    [Serializable]
    class AnnouncementMessage : Message
    {
        public ICollection<String> GameNames;
        public AnnouncementMessage(UserInfo user, ICollection<String> gameNames)
        {
            this.User = user;
            this.GameNames = gameNames;
        }
    }
}
