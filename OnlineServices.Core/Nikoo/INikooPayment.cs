using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OnlineServices.Entity;

namespace OnlineServices.Core.Nikoo
{
    public interface INikooPayment
    {
        Task<List<NikooPayment>> GetAllPayment();
        Task<int> Add(NikooPayment nikooPayment);
        Task<bool> UserHasCourse(NikooPayment nikooPayment);
        Task Delete(NikooPayment nikooPayment);
        Task FinishPayment(int paymentId, string refId);
        Task<NikooPayment> GetPaymentById(int Id);
        Task<List<NikooPayment>> GetAllUserOpnePayment(string mobileNumber);
        Task DeleteAllUserOpnePayment(List<NikooPayment> nikooPayments);
        Task<List<NikooPayment>> GetAllUserClosePayment(string mobileNumber);
    }
}
