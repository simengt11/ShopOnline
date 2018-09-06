using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class OrderDAO
    {
        OnlineShopDbContext onlineShopDbContext = null;
        public OrderDAO()
        {
            onlineShopDbContext = new OnlineShopDbContext();
        }

        public long InsertOrder(Order order)
        {
            onlineShopDbContext.Orders.Add(order);
            onlineShopDbContext.SaveChanges();
            return order.ID;
        }
    }
}
