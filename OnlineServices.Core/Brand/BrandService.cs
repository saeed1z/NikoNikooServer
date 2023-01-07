using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineServices.Entity;
using OnlineServices.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public class BrandService : IBrandService
    {
        private readonly OnlineServicesDbContext _db;
        public BrandService(OnlineServicesDbContext db)
        {
            _db = db;
        }

        public void CreateAsync(Brand newBrand)
        {
            try
            {
                _db.Brand.Add(newBrand);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(int BrandId)
        {
            var brand = GetById(BrandId);
            _db.Remove(brand);
            await _db.SaveChangesAsync();
        }

        public List<Brand> GetAll(string Title = "", string Description = "", bool IsActive = true) =>

            _db.Brand.Where(x => (x.Title.Contains(Title) || Title == "")
            && (x.Description.Contains(Description) || Description == "")
            && (x.IsActive == IsActive)
            ).ToList();

        public Brand GetById(int BrandId) => _db.Brand.Find(BrandId);

        public IPagedList<Brand> GetPagedAll(int page = 0, int pageSize = int.MaxValue)
        {
            var query = _db.Brand.ToList();
            return new PagedList<Brand>(query, page, pageSize);

        }

        public void UpdateAsync(Brand Brand)
        {
            try
            {
                _db.Update(Brand);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task UpdateAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
