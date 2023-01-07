using Microsoft.AspNetCore.Mvc.Rendering;
//using OnlineServices.Core.Implementation;
using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface IPersonService_Service
    {
        Task CreateAsync(PersonService newPersonService);
        PersonService GetById(int PersonServiceId);
        Task UpdateAsync(PersonService PersonService);
        Task UpdateAsync(int id);
        Task Delete(int PersonServiceId);
        List<PersonService> GetAll(Guid PersonId);
        List<SelectListItem> GetAllPersonServicesForPerson();
    }
}
