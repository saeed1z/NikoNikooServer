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
    public class StateService : IStateService
    {
        private readonly OnlineServicesDbContext _db;
        public StateService(OnlineServicesDbContext db)
        {
            _db = db;
        }

        public Task CreateAsync(State newState)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int StateId)
        {
            throw new NotImplementedException();
        }

        public List<State> GetAll() => _db.State.ToList();

        public State GetById(int StateId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(State State)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
