using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class FeedbackDAO
    {
        OnlineShopDbContext onlineShopDbContext = null;
        public FeedbackDAO()
        {
            onlineShopDbContext = new OnlineShopDbContext();
        }

        public bool InsertFeedback(Feedback feedback)
        {
            onlineShopDbContext.Feedbacks.Add(feedback);
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
