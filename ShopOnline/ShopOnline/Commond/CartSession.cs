using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Commond
{
    [Serializable]
    public class CartSession
    {
        public Product product { get; set; }
        public int Quanlity { get; set; }
    }
}