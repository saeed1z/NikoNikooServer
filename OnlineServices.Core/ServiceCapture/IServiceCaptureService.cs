using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineServices.Entity;

namespace OnlineServices.Core
{
    public interface IServiceCaptureService
    {
        ServiceCapture GetById(System.Guid Id);
        List<ServiceCapture> LoadData(int SkipRow = 0, int pageSize = int.MaxValue);
        string CreateAsync(ServiceCapture newServiceCapture);
        void UpdateAsync(ServiceCapture _ServiceCapture);
        void Delete(System.Guid Id);
    }
}
