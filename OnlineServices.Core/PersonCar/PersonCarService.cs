using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
    public class PersonCarService : IPersonCarService
    {
        private readonly OnlineServicesDbContext _db;
        public PersonCarService(OnlineServicesDbContext db)
        {
            _db = db;
        }

        public void CreateAsync(PersonCar newPersonCar)
        {
            try
            {
                _db.PersonCar.Add(newPersonCar);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateAsync(PersonCar PersonCar)
        {
            try
            {
                _db.Update(PersonCar);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PersonCar GetById(int PersonCarId) => _db.PersonCar.Find(PersonCarId);
        public PersonCar GetByIdAndPersonId(int PersonCarId, Guid PersonId) => _db.PersonCar.FirstOrDefault(x => x.PersonId == PersonId && x.Id == PersonCarId);

        public List<PersonCar> GetAll(Guid PersonId, int? ModelId = null, string PlaqueNo = "",
            string ChassisNo = "", string Description = "", bool IsActive = true) =>
            _db.PersonCar.Where(x => (x.PersonId == PersonId)
            && (x.ModelId == ModelId || ModelId == null)
            && (x.PlaqueNo.Contains(PlaqueNo) || PlaqueNo == "")
            && (x.ChassisNo.Contains(ChassisNo) || ChassisNo == "")
            && (x.Description.Contains(Description) || Description == "")
            && (x.IsActive == IsActive)
            ).ToList();
        public IPagedList<PersonCar> GetPagedAll(int page = 0, int pageSize = int.MaxValue)
        {
            var query = _db.PersonCar.ToList();
            return new PagedList<PersonCar>(query, page, pageSize);
        }

        public async Task Delete(int Id)
        {
            var personCar = GetById(Id);
            _db.Remove(personCar);
            await _db.SaveChangesAsync();
        }

        public void Delete(PersonCar PersonCar)
        {
            _db.Remove(PersonCar);
            _db.SaveChanges();
        }
    }
}
