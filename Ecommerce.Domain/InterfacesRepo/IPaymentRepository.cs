using Ecommerce_System.Ecommerce.Domain.Models;

namespace Ecommerce_System.Ecommerce.Domain.InterfacesRepo
{
    public interface IPaymentRepository
    {
        Task<Payment> GetOrderPayment(int OrderId);
        Task<Payment> CreateAsync(Payment payment);
        Task<Payment> DeleteAsync(Payment payment);
        Task<IEnumerable<Payment>> GetAllPaymentAsync();
        Task<Payment> GetPaymentByIdAsync(int PaymentId);
        Task<Payment> UpdateAsync(Payment payment);

    }
}
