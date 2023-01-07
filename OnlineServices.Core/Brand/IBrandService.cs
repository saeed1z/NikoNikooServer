using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface IBrandService
    {
        void CreateAsync(Brand newBrand);
        Brand GetById(int BrandId);
        void UpdateAsync(Brand Brand);
        Task UpdateAsync(int id);
        Task Delete(int BrandId);
        List<Brand> GetAll(string Title = "", string Description = "", bool IsActive = true);
        IPagedList<Brand> GetPagedAll(int page = 0, int pageSize = int.MaxValue);

    }
}
