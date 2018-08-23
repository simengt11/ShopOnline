using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.DAO
{
    public class ContentDAO
    {
        OnlineShopDbContext onlineShopDbContext = null;
        public ContentDAO()
        {
            onlineShopDbContext = new OnlineShopDbContext();
        }
        public bool InsertContent(Content content)
        {
            content.CreatedDate = DateTime.Now;
            try
            {
                onlineShopDbContext.Contents.Add(content);
                onlineShopDbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool UpdateContent(Content content)
        {
            var result = onlineShopDbContext.Contents.Find(content.ID);
            result.Name = content.Name;
            result.MetaKeywords = content.MetaKeywords;
            result.MetaDescriptions = content.MetaDescriptions;
            result.Description = content.Description;
            result.CatagoryID = content.CatagoryID;
            result.ModifiedDate = DateTime.Now;
            result.ModifiedBy = content.ModifiedBy;
            result.Status = content.Status;
            result.Tags = content.Tags;
            result.TopHot = content.TopHot;
            result.MetaTitle = content.MetaTitle;
            result.Image = content.Image;
            result.Detail = content.Detail;
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

        public bool DeleteContent(long contentID)
        {
            var content = onlineShopDbContext.Contents.Find(contentID);
            try
            {
                onlineShopDbContext.Contents.Remove(content);
                onlineShopDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public Content GetContentByID(long id)
        {
            var result = onlineShopDbContext.Contents.Find(id);
            return result;
        }

        public IEnumerable<Content> ListAllPagingContent(string searchString, int page, int pageSize)
        {
            IQueryable<Content> model = onlineShopDbContext.Contents;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Description.Contains(searchString) || x.Detail.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedBy).ToPagedList(page, pageSize);
        }

        public bool ChangeContentStatus(long? contentID)
        {
            var content = onlineShopDbContext.Contents.Find(contentID);
            if (content.Status)
                content.Status = false;
            else
                content.Status = true;
            onlineShopDbContext.SaveChanges();
            return !content.Status;
        }

    }
}
