using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineServices.Core
{
    public interface IBannerService
    {
        void Create(Banner newBanner);
        Banner GetById(Guid Id);
        void Update(Banner Banner);
        void Delete(Guid Id);
        List<Banner> GetAll();
    }
}
