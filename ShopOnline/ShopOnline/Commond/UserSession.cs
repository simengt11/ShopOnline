using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Commond
{
    [Serializable]
    public class UserSession
    {
        public int UserID { get; set; }
        public string Username  { get; set; }
    }
}