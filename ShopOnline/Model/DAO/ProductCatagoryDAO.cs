using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ProductCatagoryDAO
    {
        OnlineShopDbContext onlineShopDbContext = null;
        public ProductCatagoryDAO()
        {
            onlineShopDbContext = new OnlineShopDbContext();
        }
        public List<ProductCatagory> GetCatagories()
        {

            var result = onlineShopDbContext.ProductCatagories.Where(x => x.Status == true).ToList();
            return result;
        }

        public bool InsertProductCatagory(ProductCatagory product)
        {
            product.CreatedDate = DateTime.Now;
            try
            {
                onlineShopDbContext.ProductCatagories.Add(product);
                onlineShopDbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool UpdateProductCatagory(ProductCatagory product)
        {
            var result = onlineShopDbContext.ProductCatagories.Find(product.ID);
            result.Name = product.Name;
            result.MetaTitle = product.MetaTitle;
            result.MetaDescriptions = product.MetaDescriptions;
            result.ParentID = product.ParentID;
            result.ModifiedDate = DateTime.Now;
            result.ModifiedBy = product.ModifiedBy;
            result.MetaKeywords = product.MetaKeywords;
            result.SeoTitle = product.SeoTitle;
            result.DisplayOrder = product.DisplayOrder;
            result.Status = product.Status;
            try
            {
                onlineShopDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exeption when update product catagory: {0}", e);
                return false;
            }

            return true;
        }

        public bool DeleteProductCatagory(long productID)
        {
            var product = onlineShopDbContext.ProductCatagories.Find(productID);
            try
            {
                onlineShopDbContext.ProductCatagories.Remove(product);
                onlineShopDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public ProductCatagory GetProductCatagoryByID(long id)
        {
            var result = onlineShopDbContext.ProductCatagories.Find(id);
            return result;
        }

        public List<ProductCatagory> GetAllCatagories()
        {
            return onlineShopDbContext.ProductCatagories.Where(x=>x.Status==true).OrderBy(x=>x.DisplayOrder).ToList();
        }

        public IEnumerable<ProductCatagory> ListAllPagingProductCatagory(string searchString, int page, int pageSize)
        {
            IQueryable<ProductCatagory> model = onlineShopDbContext.ProductCatagories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedBy).ToPagedList(page, pageSize);
        }

        public bool ChangeProductCatagoryStatus(long? productID)
        {
            var product = onlineShopDbContext.ProductCatagories.Find(productID);
            if (product.Status)
                product.Status = false;
            else
                product.Status = true;
            onlineShopDbContext.SaveChanges();
            return !product.Status;
        }

        public bool ChangeProductCatagoryShowOnHome(long? productID)
        {
            var product = onlineShopDbContext.ProductCatagories.Find(productID);
            if (product.ShowOnHome)
                product.ShowOnHome = false;
            else
                product.ShowOnHome = true;
            onlineShopDbContext.SaveChanges();
            return !product.ShowOnHome;
        }
    }
}
