using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface IModelService
    {
        void CreateAsync(Model newModel);
        Model GetById(int ModelId);
        void UpdateAsync(Model Model);
        Task UpdateAsync(int id);
        Task Delete(int ModelId);
        List<Model> GetAll(int BrandId = 0, int CarTypeBaseId = 0, string Title = "", string Description = "", decimal? Price = null, decimal? FromPrice = null, decimal? ToPrice = null, bool IsActive = true);
        IPagedList<Model> GetPagedAll(int page = 0, int pageSize = int.MaxValue);
        string GetTechnicalInfoValue(int ModelId, int BaseId);
        ModelTechnicalInfo GetTechnicalInfo(int ModelId, int BaseId);
        void UpdateTechnicalInfo(ModelTechnicalInfo ModelTechnicalInfo);
        void CreateTechnicalInfo(ModelTechnicalInfo newModelTechnicalInfo);
        List<ModelTechnicalInfo> GetAllTechnicalInfo(int ModelId);
        IPagedList<ModelGallery> GetPagedAllModelGallery(int ModelId, int page = 0, int pageSize = int.MaxValue);
        ModelGallery GetModelGalleryById(Guid ModelGalleryId);
        void CreateModelGallery(ModelGallery newModelGallery);
        void UpdateModelGallery(ModelGallery ModelGallery);
        Task DeleteModelGallery(ModelGallery ModelGallery);
    }
}
