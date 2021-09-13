using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Common
{
    [Serializable]
    public class UserLogin
    {
        public long userID { set; get; }
        public string userName { set; get; }
        public string phoneNumbers { set; get; }
        public string adress { set; get; }
        public string email { set; get; }
        public string name { set; get; }
    }
}