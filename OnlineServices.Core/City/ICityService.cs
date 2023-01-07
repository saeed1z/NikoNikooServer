using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface ICityService
    {
        Task CreateAsync(City newCity);
        City GetById(int CityId);
        Task UpdateAsync(City City);
        Task UpdateAsync(int id);
        Task Delete(int CityId);
        List<City> GetAll();
        List<SelectListItem> GetAllCitisForState(int? StateId, int? CityId);
    }
}
