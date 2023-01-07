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
    public class BaseKindService : IBaseKindService
    {
        private readonly OnlineServicesDbContext _db;
        public BaseKindService(OnlineServicesDbContext db)
        {
            _db = db;
        }

        public void CreateAsync(BaseKind newBaseKind)
        {
            try
            {
                _db.BaseKind.Add(newBaseKind);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(int BaseKindId)
        {
            var objBaseKind = GetById(BaseKindId);
            _db.Remove(objBaseKind);
            await _db.SaveChangesAsync();
        }

        public List<BaseKind> GetAll(int? ParentBaseKindId = null) =>
            _db.BaseKind.Where(x => (x.ParentBaseKindId == ParentBaseKindId || ParentBaseKindId == null)).OrderBy(x => x.OrderNo).ToList();

        public BaseKind GetById(int BaseKindId) => _db.BaseKind.Find(BaseKindId);

        public void UpdateAsync(BaseKind BaseKind)
        {
            try
            {
                _db.Update(BaseKind);
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

        public IPagedList<BaseKind> GetPagedAll(int? ParentBaseKindId = null, int page = 0, int pageSize = int.MaxValue)
        {
            var query = _db.BaseKind.Where(x => (x.ParentBaseKindId == ParentBaseKindId || ParentBaseKindId == null)).ToList();
            return new PagedList<BaseKind>(query, page, pageSize);

        }
    }
}
