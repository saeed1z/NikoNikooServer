using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OnlineServices.Entity;
using OnlineServices.Persistence;
using OnlineServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace OnlineServices.Core
{
    public class PersonServices : IPersonService
    {
        private readonly OnlineServicesDbContext _db;

        private readonly IAspNetUserService _aspNetUser;

        public PersonServices(OnlineServicesDbContext db, IAspNetUserService aspNetUser)
        {
            _db = db;
            _aspNetUser = aspNetUser;
        }
        public async Task Delete(Guid Id)
        {
            var person = GetById(Id);
            _db.Remove(person);
            await _db.SaveChangesAsync();
        }
        public List<Person> GetAll() => _db.Person.ToList();

        public List<Person> GetRefrenceUserForApp(int PersonTypeId = 0, int serviceTypeId = 0, double Latitude = 0, double Longitude = 0)
        {
            var personServiceList = _db.PersonService.Where(x => x.ServiceTypeId == serviceTypeId)
                .Select(x => x.PersonId).ToList();

            var query = (from person in _db.Person
                         where personServiceList.Contains(person.Id)
                               && (person.PersonTypeId == PersonTypeId || PersonTypeId == 0)
                         orderby Math.Pow((Math.Pow((person.Latitude.Value - Latitude), 2) + Math.Pow((person.Longitude.Value - Longitude), 2)), 0.5)
                         select person).ToList();

            return query;
        }
        public IPagedList<Person> GetPagedAll(int page = 0, int pageSize = int.MaxValue)
        {
            int normalUser = (int)PersonTypeEnum.NormalUser;
            int commercialUser = (int)PersonTypeEnum.CommercialUser;
            List<Person> query = _db.Person.Where(x => x.PersonTypeId != normalUser && x.PersonTypeId != commercialUser).ToList();
            return new PagedList<Person>(query, page, pageSize);

        }
        public IPagedList<Person> GetPagedAllUsers(int page = 0, int pageSize = int.MaxValue)
        {
            int normalUser = (int)PersonTypeEnum.NormalUser;
            int commercialUser = (int)PersonTypeEnum.CommercialUser;
            var query = _db.Person.Where(x => x.PersonTypeId == normalUser || x.PersonTypeId == commercialUser).ToList();
            return new PagedList<Person>(query, page, pageSize);

        }

        public List<EmployeeForExpert> GetAllEmployees(int serviceTypeId)
        {
            //var persons = _db.Person.Where(x => x.PersonTypeId == (byte)PersonTypeEnum.Employee && (serviceTypeId==0 || x.PersonService.Any(p=>p.ServiceTypeId==serviceTypeId)))
            //    .Select(y => new EmployeeForExpert()
            //    {
            //        Id = y.Id,
            //        FirstName = y.FirstName,
            //        LastName = y.LastName,
            //        MobileNo = y.MobileNo
            //    }).ToList();

            var persons = _db.PersonService.Where(x => x.ServiceTypeId == serviceTypeId && x.Person.PersonTypeId == (byte)PersonTypeEnum.Employee)
            .Select(s => new EmployeeForExpert()
            {
                Id = s.Person.Id,
                FirstName = s.Person.FirstName,
                LastName = s.Person.LastName,
                MobileNo = s.Person.MobileNo
            })
            .ToList();

            return persons;
        }



        public IPagedList<Person> GetPagedRefrenceUser(int PersonTypeId = 0, int serviceTypeId = 0, int stateId = 0, int cityId = 0, int page = 0, int pageSize = int.MaxValue)
        {
            //int normalUser = (int)PersonTypeEnum.NormalUser;
            //int commercialUser = (int)PersonTypeEnum.CommercialUser;

            var personServiceList = _db.PersonService.Where(x => x.ServiceTypeId == serviceTypeId)
                .Select(x => x.PersonId).ToList();

            var query = (from person in _db.Person
                         where personServiceList.Contains(person.Id)
                               && person.StateId == stateId
                               && person.CityId == cityId
                               && (person.PersonTypeId == PersonTypeId || PersonTypeId == 0)
                         select person).ToList();

            return new PagedList<Person>(query, page, pageSize);
        }

        public Person GetById(Guid Id) => _db.Person.Find(Id);
        public Person GetByMobileNo(string MobileNo) => _db.Person.FirstOrDefault(x => x.MobileNo == MobileNo);

        public Person GetByToken(string Token)
        {
            var person = new Person();
            var userToken = _db.UserTokens.FirstOrDefault(x => x.Value == Token);
            if (userToken != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.Id == userToken.UserId);
                if (user != null)
                    person = GetByMobileNo(user.UserName);
            }
            return person;
        }

        public void CreateAsync(Person NewPerson, List<int> PersonServiceIds)
        {
            using TransactionScope scope = new TransactionScope();
            try
            {
                _db.Person.Add(NewPerson);

                if (NewPerson.PersonTypeId != (int)PersonTypeEnum.NormalUser
                    && NewPerson.PersonTypeId != (int)PersonTypeEnum.CommercialUser)
                {
                    if (PersonServiceIds != null)
                    {
                        if (PersonServiceIds.Count > 0)
                        {
                            foreach (var item in PersonServiceIds)
                            {
                                PersonService objPersonService = new PersonService
                                {
                                    ServiceTypeId = item,
                                    PersonId = NewPerson.Id,
                                    CreatedDate = DateTime.Now,
                                    CreatedUserId = NewPerson.CreatedUserId.Value
                                };
                                _db.PersonService.Add(objPersonService);
                            }
                        }
                    }
                }

                _db.SaveChanges();
                scope.Complete();
            }
            catch (Exception ex)
            {
                scope.Dispose();
            }
        }
        public void Update(Person Person)
        {
            try
            {
                _db.Update(Person);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateAsync(Person Person, List<int> PersonServiceIds)
        {
            using TransactionScope scope = new TransactionScope();
            try
            {
                //var NewPersonServiceList = Person.PersonService.ToList();
                _db.Update(Person);

                if (Person.PersonTypeId != (int)PersonTypeEnum.NormalUser
                     && Person.PersonTypeId != (int)PersonTypeEnum.CommercialUser)
                {
                    if (PersonServiceIds != null)
                    {
                        var oldPersonServiceList = _db.PersonService.Where(x => x.PersonId == Person.Id).ToList();
                        if (oldPersonServiceList.Count > 0)
                            //{
                            foreach (var item in oldPersonServiceList)
                                _db.PersonService.Remove(item);
                        //}
                        if (PersonServiceIds.Count > 0)
                        {
                            foreach (var item in PersonServiceIds)
                            {
                                PersonService objPersonService = new PersonService
                                {
                                    ServiceTypeId = item,
                                    PersonId = Person.Id,
                                    UpdatedDate = DateTime.Now,
                                    UpdatedUserId = Person.CreatedUserId.Value
                                };
                                _db.PersonService.Add(objPersonService);
                            }
                        }
                    }
                }
                _db.SaveChanges();
                scope.Complete();
            }
            catch (Exception)
            {
                scope.Dispose();
            }
        }
        public async Task UpdateAsync(Guid Id)
        {
            Person person = GetById(Id);
            _db.Update(person);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> IsFullProfileAsync(Person person)
            => await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(person.PhoneNo))
                    return false;

                if (string.IsNullOrEmpty(person.NationalCode))
                    return false;

                if (string.IsNullOrEmpty(person.MobileNo))
                    return false;

                if (!person.IsActive)
                    return false;

                if (string.IsNullOrEmpty(person.LastName))
                    return false;

                if (string.IsNullOrEmpty(person.FatherName))
                    return false;

                if (string.IsNullOrEmpty(person.BuildingPlate))
                    return false;
                              
                if (string.IsNullOrEmpty(person.FirstName))
                    return false;

                if (string.IsNullOrEmpty(person.Email))
                    return false;

                if (string.IsNullOrEmpty(person.BirthDate))
                    return false;

                if (string.IsNullOrEmpty(person.Gender))
                    return false;

                if (string.IsNullOrEmpty(person.CityArea))
                    return false;

                if (string.IsNullOrEmpty(person.Address))
                    return false;

                if (person.BuildingFloor == null)
                    return false;

                if (person.BuildingUnit == null)
                    return false;

                if (person.CityId == null)
                    return false;

                if (person.StateId == null)
                    return false;

                return true;

            });

        public async Task<bool> IsFullProfileAsync(Guid personId)
         => await Task.FromResult(await IsFullProfileAsync(GetById(personId)));

        public async Task<Person> GetPersonFromTokenAsync(IHeaderDictionary header)
            => await Task.Run(() =>
            {
                string token = header["Token"];
                if (!string.IsNullOrEmpty(token))
                {
                    IdentityUser user = _aspNetUser.GetUserByToken(token);
                    if (user != null)
                        return GetByMobileNo(user.PhoneNumber);
                    return null;
                }
                return null;
            });
    }
}
