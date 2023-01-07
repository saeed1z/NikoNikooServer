using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface IBaseService
    {
        void CreateAsync(Base newBase);
        Base GetById(int BaseId);
        void UpdateAsync(Base Base);
        Task UpdateAsync(int id);
        Task Delete(int BaseId);
        List<Base> GetAll(int BaseKindId = 0);
        IPagedList<Base> GetPagedAll(int BaseKindId, int page = 0, int pageSize = int.MaxValue);

    }
}
