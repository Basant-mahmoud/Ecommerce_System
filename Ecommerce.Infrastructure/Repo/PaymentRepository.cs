using Ecommerce_System.Ecommerce.Domain.InterfacesRepo;
using Ecommerce_System.Ecommerce.Domain.Models;
using Ecommerce_System.Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_System.Ecommerce.Infrastructure.Repo
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly EcommerceDBContext _dbContext;
        public PaymentRepository(EcommerceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Payment> CreateAsync(Payment payment)
        {
            await _dbContext.Payments.AddAsync(payment);
            await _dbContext.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment> DeleteAsync(Payment payment)
        {
            _dbContext.Payments.Remove(payment);
            await _dbContext.SaveChangesAsync();
            return payment;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentAsync()
        {
            var payments = await _dbContext.Payments.ToListAsync();
            return payments;
        }

        public async Task<Payment> GetPaymentByIdAsync(int PaymentId)
        {
            var result = await _dbContext.Payments.FirstOrDefaultAsync(p => p.Id == PaymentId);
            return result;

        }

        public async Task<Payment> GetOrderPayment(int OrderId)
        {
            var payment = await _dbContext.Payments
           .FirstOrDefaultAsync(p => p.OrderId == OrderId);

            return payment;
        }

        public async Task<Payment> UpdateAsync(Payment payment)
        {
            _dbContext.Update(payment);
            await _dbContext.SaveChangesAsync();
            return payment;
        }
    }
}