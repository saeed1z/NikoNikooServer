using Microsoft.AspNetCore.Http;
using OnlineServices.Entity;
using OnlineServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface IPersonService
    {
        void CreateAsync(Person NewPerson, List<int> PersonServiceIds);
        Person GetById(Guid Id);
        Person GetByToken(string Token);
        Person GetByMobileNo(string MobileNo);
        void Update(Person Person);
        void UpdateAsync(Person Person, List<int> PersonServiceIds);
        Task UpdateAsync(Guid Id);
        Task Delete(Guid Id);
        List<Person> GetAll();

        Task<Person> GetPersonFromTokenAsync(IHeaderDictionary header);

        Task<bool> IsFullProfileAsync(Person person);

        Task<bool> IsFullProfileAsync(Guid personId);

        List<Person> GetRefrenceUserForApp(int PersonTypeId = 0, int serviceTypeId = 0, double Latitude = 0, double Longitude = 0);

        List<EmployeeForExpert> GetAllEmployees(int serviceTypeId);
        IPagedList<Person> GetPagedAll(int page = 0, int pageSize = int.MaxValue);

        IPagedList<Person> GetPagedAllUsers(int page = 0, int pageSize = int.MaxValue);

        IPagedList<Person> GetPagedRefrenceUser(int PersonTypeId = 0, int serviceTypeId = 0, int stateId = 0, int cityId = 0, int page = 0, int pageSize = int.MaxValue);

    }
}
