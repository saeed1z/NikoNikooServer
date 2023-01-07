using OnlineServices.Entity;
using OnlineServices.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineServices.Core
{
    public class BannerService : IBannerService
    {
        private readonly OnlineServicesDbContext _db;
        public BannerService(OnlineServicesDbContext db)
        {
            _db = db;
        }
        public void Create(Banner newBanner)
        {
            try
            {
                _db.Banner.Add(new Banner
                {
                    Id = new Guid(),
                    BannerFile = newBanner.BannerFile,
                    RowNum = newBanner.RowNum,
                    Title = newBanner.Title
                });

                _db.SaveChanges();
            }

            catch (Exception e)
            {
                throw e;
            }

        }
        public Banner GetById(Guid Id)
        {
            return _db.Banner.Find(Id);
        }
        public void Update(Banner Banner)
        {
            try
            {
                _db.Update(Banner);
                _db.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }

        }
        public void Delete(Guid Id)
        {
            try
            {
                var banner = _db.Banner.Find(Id);
                _db.Remove(banner);
                _db.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
            

        }
        public List<Banner> GetAll()
        {
            return _db.Banner.OrderBy(x=>x.RowNum).ToList();
        }
    }
}
