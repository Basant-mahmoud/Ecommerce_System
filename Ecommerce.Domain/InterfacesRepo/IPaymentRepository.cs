using Ecommerce_System.Ecommerce.Domain.Models;

namespace Ecommerce_System.Ecommerce.Domain.InterfacesRepo
{
    public interface IPaymentRepository
    {
        Task <Payment> CreateAsync (Payment payment);
        Task<Payment> UpdateAsync (Payment payment);
        Task<Payment> DeleteAsync(Payment payment);
        Task<Payment> GetPaymentByIdAsync(int PaymentId);
        Task<IEnumerable<Payment>> GetAllPaymentAsync();
        Task<Payment> GetUserPayment(int OrderId);
    }
}
