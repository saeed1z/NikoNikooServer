using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface IStateService
    {
        Task CreateAsync(State newState);
        State GetById(int StateId);
        Task UpdateAsync(State State);
        Task UpdateAsync(int id);
        Task Delete(int StateId);
        List<State> GetAll();

    }
}
