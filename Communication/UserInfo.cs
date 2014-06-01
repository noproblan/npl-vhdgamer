using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VhdGamer.Communication
{
    [Serializable]
    public class UserInfo
    {
        public String Name;
        public UserInfo(string name)
        {
            this.Name = name;
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
