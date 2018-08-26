using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class SlideDAO
    {

        OnlineShopDbContext onlineShopDbContext = null;
        public SlideDAO()
        {
            onlineShopDbContext = new OnlineShopDbContext();
        }

        public bool InsertSlide(Slide slide)
        {
            slide.CreatedDate = DateTime.Now;
            try
            {
                onlineShopDbContext.Slides.Add(slide);
                onlineShopDbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool UpdateSlide(Slide slide)
        {
            var result = onlineShopDbContext.Slides.Find(slide.ID);
            result.FirstTitle = slide.FirstTitle;
            result.SecondTitle = slide.SecondTitle;
            result.Image = slide.Image;
            result.Link = slide.Link;
            result.ModifiedDate = DateTime.Now;
            result.ModifiedBy = slide.ModifiedBy;
            result.MetaKeywords = slide.MetaKeywords;
            result.Description = slide.Description;
            result.DisplayOrder = slide.DisplayOrder;
            result.Status = slide.Status;
            try
            {
                onlineShopDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exeption when update slide: {0}", e);
                return false;
            }

            return true;
        }

        public bool DeleteSlide(long slideID)
        {
            var slide = onlineShopDbContext.Slides.Find(slideID);
            try
            {
                onlineShopDbContext.Slides.Remove(slide);
                onlineShopDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public Slide GetSlideByID(int id)
        {
            var result = onlineShopDbContext.Slides.Find(id);
            return result;
        }

        public List<Slide> GetAllSlide()
        {
            return onlineShopDbContext.Slides.Where(x => x.Status == true).OrderBy(x => x.CreatedDate).ToList();
        }

        public IEnumerable<Slide> ListAllPagingSlide(string searchString, int page, int pageSize)
        {
            IQueryable<Slide> model = onlineShopDbContext.Slides;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.FirstTitle.Contains(searchString) || x.SecondTitle.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedBy).ToPagedList(page, pageSize);
        }

        public bool ChangeSlideStatus(long? sildeID)
        {
            var slide = onlineShopDbContext.Slides.Find(sildeID);
            if (slide.Status)
                slide.Status = false;
            else
                slide.Status = true;
            onlineShopDbContext.SaveChanges();
            return !slide.Status;
        }
    }
}
