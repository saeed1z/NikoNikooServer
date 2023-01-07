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
    public class BaseService : IBaseService
    {
        private readonly OnlineServicesDbContext _db;
        public BaseService(OnlineServicesDbContext db)
        {
            _db = db;
        }

        public void CreateAsync(Base newBase)
        {
            try
            {
                _db.Base.Add(newBase);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(int BaseId)
        {
            var objBase = GetById(BaseId);
            _db.Remove(objBase);
            await _db.SaveChangesAsync();
        }

        public List<Base> GetAll(int BaseKindId = 0) =>
            _db.Base.Where(x => (x.BaseKindId == BaseKindId || BaseKindId == 0)).OrderBy(x => x.OrderNo).ToList();

        public Base GetById(int BaseId) => _db.Base.Find(BaseId);

        public void UpdateAsync(Base Base)
        {
            try
            {
                _db.Update(Base);
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

        public IPagedList<Base> GetPagedAll(int BaseKindId, int page = 0, int pageSize = int.MaxValue)
        {
            var query = _db.Base.Where(x => x.BaseKindId == BaseKindId).ToList();
            return new PagedList<Base>(query, page, pageSize);

        }
    }
}
