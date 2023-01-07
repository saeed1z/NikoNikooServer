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
    public class PersonService_Service : IPersonService_Service
    {
        private readonly OnlineServicesDbContext _db;
        public PersonService_Service(OnlineServicesDbContext db)
        {
            _db = db;
        }

        public Task CreateAsync(PersonService newPersonService)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int PersonServiceId)
        {
            throw new NotImplementedException();
        }

        public List<PersonService> GetAll(Guid PersonId) => 
            _db.PersonService.Where(x => x.PersonId == PersonId).ToList();

        public List<SelectListItem> GetAllPersonServicesForPerson()
        {
            throw new NotImplementedException();
        }

        public PersonService GetById(int PersonServiceId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(PersonService PersonService)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
