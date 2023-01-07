using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface IBaseKindService
    {
        void CreateAsync(BaseKind newBaseKind);
        BaseKind GetById(int BaseKindId);
        void UpdateAsync(BaseKind BaseKind);    
        Task UpdateAsync(int id);
        Task Delete(int BaseKindId);
        List<BaseKind> GetAll(int? ParentBaseKindId = null);
        IPagedList<BaseKind> GetPagedAll(int? ParentBaseKindId = null, int page = 0, int pageSize = int.MaxValue);

    }
}
