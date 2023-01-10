using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineServices.Entity;
using OnlineServices.Persistence;

namespace OnlineServices.Core.Nikoo
{
    public class NikooPaymentrRpository : INikooPayment
    {
        private readonly OnlineServicesDbContext _context;
        public NikooPaymentrRpository(OnlineServicesDbContext db)
        {
            _context = db;
        }
        public async Task<List<NikooPayment>> GetAllPayment()
        {
            return await _context.NikooPayments.Where(x => x.IsFinally == true).ToListAsync();
        }

        public async Task<bool> UserHasCourse(NikooPayment nikooPayment)
        {
            return await _context.NikooPayments.AnyAsync(x => x.UserMobile == nikooPayment.UserMobile &&
                                                        x.CourseId == nikooPayment.CourseId
                                                        && x.IsFinally == true);
        }
        public async Task<int> Add(NikooPayment nikooPayment)
        {
            NikooPayment NewPay = new NikooPayment();
            NewPay.CourseId = nikooPayment.CourseId;
            NewPay.UserMobile = nikooPayment.UserMobile;
            NewPay.IsFinally = false;
            NewPay.Price = nikooPayment.Price;
            NewPay.RefId = null;
            await _context.AddAsync(NewPay);
            await _context.SaveChangesAsync();
            return NewPay.Id;
        }
        public async Task Delete(NikooPayment nikooPayment)
        {
            _context.Remove(nikooPayment);
            await _context.SaveChangesAsync();
        }

        public async Task FinishPayment(int paymentId, string refId)
        {
            var pay = await GetPaymentById(paymentId);
            pay.RefId = refId;
            pay.IsFinally = true;
            _context.NikooPayments.Update(pay);
            await _context.SaveChangesAsync();
        }

        public async Task<NikooPayment> GetPaymentById(int Id)
        {
            return await _context.NikooPayments.FirstOrDefaultAsync(x => x.Id == Id && x.IsFinally == false);
        }

        public async Task<List<NikooPayment>> GetAllUserOpnePayment(string mobileNumber)
        {
            return await _context.NikooPayments.Where(x => x.UserMobile == mobileNumber && x.IsFinally == false)
                .ToListAsync();
        }
        public async Task DeleteAllUserOpnePayment(List<NikooPayment> nikooPayments)
        {
            foreach (var item in nikooPayments)
            {
                await Delete(item);
            }
        }
        public async Task<List<NikooPayment>> GetAllUserClosePayment(string mobileNumber)
        {
            return await _context.NikooPayments.Where(x => x.UserMobile == mobileNumber && x.IsFinally == true)
                .ToListAsync();
        }

    }
}
