using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface IPersonTypeService
    {
        List<PersonType> GetAll(bool? PanelAccess, bool? ServiceAppAccess, bool? UserAppAccess, bool? IsActive = null);
        IPagedList<PersonType> GetPagedAll(bool? PanelAccess, bool? ServiceAppAccess, bool? UserAppAccess, bool? IsActive = null, int page = 0, int pageSize = int.MaxValue);
        PersonType GetById(byte id);
    }
}
