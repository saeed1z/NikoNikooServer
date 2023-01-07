using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineServices.Entity;
using OnlineServices.Persistence;

namespace OnlineServices.Core
{
    public class ServiceCaptureService : IServiceCaptureService
    {
        private readonly OnlineServicesDbContext _db;
        public ServiceCaptureService(OnlineServicesDbContext db)
        {
            _db = db;
        }

		public ServiceCapture GetById(System.Guid Id) 
			=> _db.ServiceCapture.Find(Id);
		
        public List<ServiceCapture> LoadData(int SkipRow = 0, int pageSize = int.MaxValue)
            => _db.ServiceCapture.Skip(SkipRow).Take(pageSize).ToList();


        public string CreateAsync(ServiceCapture newServiceCapture)
        {
            string Id = null;
			try
			{
				_db.ServiceCapture.Add(newServiceCapture);
				_db.SaveChanges();
                Id = newServiceCapture.Id.ToString();

            }
			catch (Exception ex)
			{
				throw(ex);
			}
            return Id;
        }

        public void UpdateAsync(ServiceCapture _ServiceCapture)
        {
			try
			{
				_db.Update(_ServiceCapture);
				_db.SaveChanges();
			}
			catch (Exception ex)
			{
			throw(ex);
			}
        }

        public void Delete(System.Guid Id)
        {
            ServiceCapture _ServiceCapture = this.GetById(Id);
            _db.Remove(_ServiceCapture);
            _db.SaveChanges();
        }
    }
}
