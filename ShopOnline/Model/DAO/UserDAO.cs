using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;
namespace Model.DAO
{
    public class UserDAO
    {
        OnlineShopDbContext onlineShopDbContext = null;
        public UserDAO()
        {
            onlineShopDbContext = new OnlineShopDbContext();
        }
        public long InsertUser(User user)
        {
            user.CreatedDate = DateTime.Now;
            onlineShopDbContext.Users.Add(user);
            onlineShopDbContext.SaveChanges();
            return user.ID;
        }

        public bool UpdateUser(User userInfor)
        {
            var User = onlineShopDbContext.Users.Find(userInfor.ID);
            if (!string.IsNullOrEmpty(userInfor.Password))
            {
                User.Password = userInfor.Password;
            }
            User.Name = userInfor.Name;
            User.Address = userInfor.Address;
            User.ModifiedDate = DateTime.Now;
            User.ModifiedBy = userInfor.ModifiedBy;
            User.Satus = userInfor.Satus;
            try
            {
                onlineShopDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exeption when update UserInfor: {0}", e);
                return false;
            }

            return true;
        }

        public bool DeleteUser(int userID)
        {
            var user = onlineShopDbContext.Users.Find(userID);
            try
            {
                onlineShopDbContext.Users.Remove(user);
                onlineShopDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        public bool CheckUserExists(string userName)
        {
            var result = onlineShopDbContext.Users.Count(x => x.Username.Equals(userName));
            if (result > 0)
                return true;
            return false;
        }
        public bool UserLogin(string username, string password)
        {
            var result = onlineShopDbContext.Users.Count(x => x.Username.Equals(username) && x.Password.Equals(password));
            if (result > 0)
                return true;
            return false;
        }
        public User GetUserByUsername(string Username)
        {
            return onlineShopDbContext.Users.SingleOrDefault(x => x.Username.Equals(Username));
        }

        public User GetUSerByUserID(int? userID)
        {
            return onlineShopDbContext.Users.Find(userID);
        }

        public IEnumerable<User> ListAllPagingUser(string searchString, int page, int pageSize)
        {
            IQueryable<User> model = onlineShopDbContext.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Username.Contains(searchString) || x.Name.Contains(searchString) || x.Address.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public bool ChangeUserStatus(long? userID)
        {
            var user = onlineShopDbContext.Users.Find(userID);
            if (user.Satus)
                user.Satus = false;
            else
                user.Satus = true;
            onlineShopDbContext.SaveChanges();
            return !user.Satus;
        }
    }
}
