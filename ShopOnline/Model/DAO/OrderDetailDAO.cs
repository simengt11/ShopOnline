using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class OrderDetailDAO
    {
        OnlineShopDbContext onlineShopDbContext = null;
        public OrderDetailDAO()
        {
            onlineShopDbContext = new OnlineShopDbContext();
        }

        public bool InsertOrderDetail(OrderDetail orderDetail)
        {
            onlineShopDbContext.OrderDetails.Add(orderDetail);
            try
            {
                onlineShopDbContext.SaveChanges();
                return true;
            }
            catch(Exception){
                return false;
            }
        }
    }
}
