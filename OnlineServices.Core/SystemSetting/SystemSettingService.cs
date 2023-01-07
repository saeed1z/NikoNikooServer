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
    public class SystemSettingService : ISystemSettingService
    {
        private readonly OnlineServicesDbContext _db;
        public SystemSettingService(OnlineServicesDbContext db)
        {
            _db = db;
        }
        public List<SystemSetting> GetAll() => _db.SystemSetting.ToList();

    }
}
