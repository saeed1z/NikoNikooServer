using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnlineServices.Entity;
using OnlineServices.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public class PersonTypeService : IPersonTypeService
    {
        private readonly OnlineServicesDbContext _db;
        public PersonTypeService(OnlineServicesDbContext db)
        {
            _db = db;
        }
        public List<PersonType> GetAll(bool? PanelAccess, bool? ServiceAppAccess, bool? UserAppAccess, bool? IsActive = null) => 
            _db.PersonType.Where(x=>(x.PanelAccess ==PanelAccess || PanelAccess == null)
            && (x.PanelAccess == PanelAccess || PanelAccess == null)
            && (x.ServiceAppAccess == ServiceAppAccess || ServiceAppAccess == null)
            && (x.UserAppAccess == UserAppAccess || UserAppAccess == null)
            && (x.IsActive == IsActive|| IsActive==null)
            ).ToList();
        public PersonType GetById(byte Id) => _db.PersonType.Find(Id);

        public IPagedList<PersonType> GetPagedAll(bool? PanelAccess, bool? ServiceAppAccess, bool? UserAppAccess, bool? IsActive = null, int page = 0, int pageSize = int.MaxValue)
        {
            var query = _db.PersonType.Where(x => (x.PanelAccess == PanelAccess || PanelAccess == null)
            && (x.PanelAccess == PanelAccess || PanelAccess == null)
            && (x.ServiceAppAccess == ServiceAppAccess || ServiceAppAccess == null)
            && (x.UserAppAccess == UserAppAccess || UserAppAccess == null)
            && (x.IsActive == IsActive || IsActive == null)
            ).ToList();
            return new PagedList<PersonType>(query, page, pageSize);

        }
    }
}
