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
    public class ModelService : IModelService
    {
        private readonly OnlineServicesDbContext _db;
        public ModelService(OnlineServicesDbContext db)
        {
            _db = db;
        }

        public void CreateAsync(Model newModel)
        {
            try
            {
                _db.Model.Add(newModel);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(int ModelId)
        {
            var model = GetById(ModelId);
            _db.Remove(model);
            await _db.SaveChangesAsync();
        }

        public List<Model> GetAll(int BrandId = 0, int CarTypeBaseId = 0, string Title = "", string Description = "",
            decimal? Price = null, decimal? FromPrice = null,
            decimal? ToPrice = null, bool IsActive = true)
        {
            var query = _db.Model.Where(x => (x.BrandId == BrandId || BrandId == 0)
            && (x.CarTypeBaseId == CarTypeBaseId || CarTypeBaseId == 0)
            );

            if (FromPrice != null && ToPrice == null)
                query = query.Where(x => x.Price > FromPrice);
            else if (FromPrice == null && ToPrice != null)
                query = query.Where(x => x.Price <= FromPrice);
            else if (FromPrice != null && ToPrice != null)
                query = query.Where(x => (x.Price > FromPrice && x.Price <= ToPrice));

            return query.ToList();
        }

        public Model GetById(int ModelId) => _db.Model.Find(ModelId);

        public IPagedList<Model> GetPagedAll(int page = 0, int pageSize = int.MaxValue)
        {
            var query = _db.Model.ToList();
            return new PagedList<Model>(query, page, pageSize);

        }

        public IPagedList<ModelGallery> GetPagedAllModelGallery(int ModelId, int page = 0, int pageSize = int.MaxValue)
        {
            var query = _db.ModelGallery.Where(x => x.ModelId == ModelId).ToList();
            return new PagedList<ModelGallery>(query, page, pageSize);

        }
        public void UpdateAsync(Model Model)
        {
            try
            {
                _db.Update(Model);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task UpdateAsync(int id)
        {
            throw new NotImplementedException();
        }
        public string GetTechnicalInfoValue(int ModelId, int BaseId)
        {
            var query = _db.ModelTechnicalInfo.FirstOrDefault(x => x.ModelId == ModelId && x.BaseId == BaseId);
            return query != null ? query.Value : "";
        }

        public ModelTechnicalInfo GetTechnicalInfo(int ModelId, int BaseId)
        {
            return _db.ModelTechnicalInfo.FirstOrDefault(x => x.ModelId == ModelId && x.BaseId == BaseId);
        }

        public void UpdateTechnicalInfo(ModelTechnicalInfo ModelTechnicalInfo)
        {
            try
            {
                _db.Update(ModelTechnicalInfo);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CreateTechnicalInfo(ModelTechnicalInfo newModelTechnicalInfo)
        {
            try
            {
                _db.ModelTechnicalInfo.Add(newModelTechnicalInfo);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ModelTechnicalInfo> GetAllTechnicalInfo(int ModelId) =>
            _db.ModelTechnicalInfo.Where(x => (x.ModelId == ModelId)
            && (x.IsActive == true)
            ).ToList();

        public ModelGallery GetModelGalleryById(Guid ModelGalleryId) => _db.ModelGallery.Find(ModelGalleryId);
        public void CreateModelGallery(ModelGallery newModelGallery)
        {
            try
            {
                _db.ModelGallery.Add(newModelGallery);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateModelGallery(ModelGallery ModelGallery)
        {
            try
            {
                _db.Update(ModelGallery);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeleteModelGallery(ModelGallery ModelGallery)
        {
            _db.Remove(ModelGallery);
            await _db.SaveChangesAsync();
        }
    }
}
