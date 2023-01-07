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
    public class CityService : ICityService
    {
        private readonly OnlineServicesDbContext _db;
        public CityService(OnlineServicesDbContext db)
        {
            _db = db;
        }

        public Task CreateAsync(City newCity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int CityId)
        {
            throw new NotImplementedException();
        }

        public List<City> GetAll() => _db.City.ToList();

        public List<SelectListItem> GetAllCitisForState(int? StateId, int? CityId)
        {
            if (StateId != null)
                return GetAll().Where(x => x.StateId == StateId).Select(c => new SelectListItem()
                {
                    Text = c.Title,
                    Value = c.Id.ToString(),
                    Selected = CityId != null ? c.Id == CityId ? true : false : false
                }).ToList();
            else
                return GetAll().Select(c => new SelectListItem()
                {
                    Text = c.Title,
                    Value = c.Id.ToString(),
                    Selected = CityId != null ? c.Id == CityId ? true : false : false
                }).ToList();
        }

        public City GetById(int CityId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(City City)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
