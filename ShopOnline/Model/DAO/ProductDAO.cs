﻿using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ProductDAO
    {
        OnlineShopDbContext onlineShopDbContext = null;
        public ProductDAO()
        {
            onlineShopDbContext = new OnlineShopDbContext();
        }

        public long InsertProduct(Product product)
        {
            product.CreatedDate = DateTime.Now;

            onlineShopDbContext.Products.Add(product);
            onlineShopDbContext.SaveChanges();
            return product.ID;
        }

        public bool UpdateProduct(Product product)
        {
            var result = onlineShopDbContext.Products.Find(product.ID);
            result.Name = product.Name;
            result.Code = product.Code;
            result.MetaTtile = product.MetaTtile;
            result.Description = product.Description;
            result.Image = product.Image;
            result.MoreImages = product.MoreImages;
            result.Price = product.Price;
            result.PromotionPrice = product.PromotionPrice;
            result.IncludeVAT = product.IncludeVAT;
            result.Quanlity = product.Quanlity;
            result.CatalogyID = product.CatalogyID;
            result.Detail = product.Detail;
            result.Waranty = product.Waranty;
            result.ModifiedDate = DateTime.Now;
            result.ModifiedBy = product.ModifiedBy;
            result.Status = product.Status;
            result.TopHot = product.TopHot;
            result.ViewCount = product.ViewCount;
            try
            {
                onlineShopDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exeption when update Cata: {0}", e);
                return false;
            }

            return true;
        }

        public bool DeleteProduct(long productID)
        {
            var product = onlineShopDbContext.Products.Find(productID);
            try
            {
                onlineShopDbContext.Products.Remove(product);
                onlineShopDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public Product GetProductByID(long id)
        {
            var result = onlineShopDbContext.Products.Find(id);
            return result;
        }

        public IEnumerable<Product> ListAllPagingProduct(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = onlineShopDbContext.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedBy).ToPagedList(page, pageSize);
        }

        public bool ChangeProductStatus(long? productID)
        {
            var product = onlineShopDbContext.Products.Find(productID);
            if (product.Status)
                product.Status = false;
            else
                product.Status = true;
            onlineShopDbContext.SaveChanges();
            return !product.Status;
        }

        public bool ChangeProductIncludeVAT(long? productID)
        {
            var product = onlineShopDbContext.Products.Find(productID);
            if (product.IncludeVAT)
                product.IncludeVAT = false;
            else
                product.IncludeVAT = true;
            onlineShopDbContext.SaveChanges();
            return !product.IncludeVAT;
        }

        public List<Product> GetNewProducts(int top)
        {
            var result = onlineShopDbContext.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
            return result;
        }

        public List<Product> GetFeatureProducts(int top)
        {
            var result = onlineShopDbContext.Products.OrderByDescending(x => x.ViewCount).Take(top).ToList();
            return result;
        }

        public List<Product> GetProductByCatagoryID(long id, ref int totalRecord, int page, int pageSize)
        {
            
            var result = onlineShopDbContext.Products.Where(x => x.CatalogyID == id);
            totalRecord = result.Count();
            return result.OrderBy(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Product> GetProductByName(string keyword, ref int totalRecord, int page, int pageSize)
        {

            var result = onlineShopDbContext.Products.Where(x => x.Name.Contains(keyword));
            totalRecord = result.Count();
            return result.OrderBy(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Product> GetRelatedProductByCatagoryID(long catagoryID, long productID)
        {
            var result= onlineShopDbContext.Products.Where(x => x.CatalogyID == catagoryID && x.ID != productID);
            return result.ToList();
        }

        public List<string> ListProductProductName(string keyword)
        {
            return onlineShopDbContext.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }

    }
}

