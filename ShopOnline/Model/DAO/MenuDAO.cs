using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class MenuDAO
    {
        OnlineShopDbContext onlineShopDbContext = null;
        public MenuDAO()
        {
            onlineShopDbContext = new OnlineShopDbContext();
        }

        public List<Menu> GetMenuByMenuTypeID(int menutypeID)
        {
            var result = onlineShopDbContext.Menus.Where(x => x.TypeID == menutypeID && x.Status == true).OrderBy(x => x.DisplayOrder);
            return result.ToList();
        }

        public bool InsertMenu(Menu menu)
        {
            //menu.CreatedDate = DateTime.Now;
            onlineShopDbContext.Menus.Add(menu);
            try
            {
                onlineShopDbContext.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
        }

        public bool UpdateMenu(Menu menu)
        {
            var Menu = onlineShopDbContext.Menus.Find(menu.ID);
            Menu.Text = menu.Text;
            Menu.Link = menu.Link;
            Menu.DisplayOrder = menu.DisplayOrder;
            Menu.Target = menu.Target;
            Menu.Status = menu.Status;
            Menu.TypeID = menu.TypeID;
            try
            {
                onlineShopDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exeption when update menu: {0}", e);
                return false;
            }

            return true;
        }

        public bool DeleteMenu(int menuID)
        {
            var menu = onlineShopDbContext.Menus.Find(menuID);
            try
            {
                onlineShopDbContext.Menus.Remove(menu);
                onlineShopDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<Menu> ListAllPagingMenu(string searchString, int page, int pageSize)
        {
            IQueryable<Menu> model = onlineShopDbContext.Menus;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Text.Contains(searchString));
            }
            return model.OrderByDescending(x => x.TypeID).ToPagedList(page, pageSize);
        }

        public bool ChangeMenuStatus(long? menuID)
        {
            var menu = onlineShopDbContext.Menus.Find(menuID);
            if (menu.Status)
                menu.Status = false;
            else
                menu.Status = true;
            onlineShopDbContext.SaveChanges();
            return !menu.Status;
        }

        public Menu GetMenuByID(int id)
        {
            var result = onlineShopDbContext.Menus.Find(id);
            return result;
        }
    }
}
