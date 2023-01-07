using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface IPersonCarService
    {
        void CreateAsync(PersonCar newPersonCar);
        void UpdateAsync(PersonCar PersonCar);
        PersonCar GetById(int PersonCarId);
        PersonCar GetByIdAndPersonId(int PersonCarId,Guid PersonId);
        List<PersonCar> GetAll(Guid PersonId, int? ModelId = null, string PlaqueNo = "", string ChassisNo = "", string Description = "", bool IsActive = true);
        IPagedList<PersonCar> GetPagedAll(int page = 0, int pageSize = int.MaxValue);
        Task Delete(int Id);
        void Delete(PersonCar PersonCar);

    }
}
