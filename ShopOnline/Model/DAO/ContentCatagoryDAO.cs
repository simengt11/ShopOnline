using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.DAO
{
    public class ContentCatagoryDAO
    {
        OnlineShopDbContext onlineShopDbContext = null;
        public ContentCatagoryDAO()
        {
            onlineShopDbContext = new OnlineShopDbContext();
        }

        public List<Catagory> GetCatagories()
        {

            var result = onlineShopDbContext.Catagories.Where(x => x.Status == true).ToList();
            return result;
        }

        public bool InsertContentCatagory(Catagory catagory)
        {
            catagory.CreatedDate = DateTime.Now;
            try
            {
                onlineShopDbContext.Catagories.Add(catagory);
                onlineShopDbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool UpdateContentCatagory(Catagory catagory)
        {
            var result = onlineShopDbContext.Catagories.Find(catagory.ID);
            result.Name = catagory.Name;
            result.MetaKeywords = catagory.MetaKeywords;
            result.MetaDescriptions = catagory.MetaDescriptions;
            result.ParentID = catagory.ParentID;
            result.ModifiedDate = DateTime.Now;
            result.ModifiedBy = catagory.ModifiedBy;
            result.Status = catagory.Status;
            result.MetaTitle = catagory.MetaTitle;
            result.DisplayOrder = catagory.DisplayOrder;
            result.IncludeVAT = catagory.IncludeVAT;
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

        public bool DeleteContentCatagory(long catagoryID)
        {
            var catagory = onlineShopDbContext.Catagories.Find(catagoryID);
            try
            {
                onlineShopDbContext.Catagories.Remove(catagory);
                onlineShopDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public Catagory GetContentCatagoryByID(long id)
        {
            var result = onlineShopDbContext.Catagories.Find(id);
            return result;
        }

        public IEnumerable<Catagory> ListAllPagingContentCatagory(string searchString, int page, int pageSize)
        {
            IQueryable<Catagory> model = onlineShopDbContext.Catagories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedBy).ToPagedList(page, pageSize);
        }

        public bool ChangeContentCatagoryStatus(long? contentID)
        {
            var catagory = onlineShopDbContext.Catagories.Find(contentID);
            if (catagory.Status)
                catagory.Status = false;
            else
                catagory.Status = true;
            onlineShopDbContext.SaveChanges();
            return !catagory.Status;
        }

        public bool ChangeContentCatagoryShowOnHome(long? contentID)
        {
            var catagory = onlineShopDbContext.Catagories.Find(contentID);
            if (catagory.IncludeVAT)
                catagory.IncludeVAT = false;
            else
                catagory.IncludeVAT = true;
            onlineShopDbContext.SaveChanges();
            return !catagory.IncludeVAT;
        }
    }
}

