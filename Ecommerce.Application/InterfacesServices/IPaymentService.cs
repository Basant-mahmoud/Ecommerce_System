using Ecommerce_System.Ecommerce.Domain.DTO;
using Ecommerce_System.Ecommerce.Domain.Models;

namespace Ecommerce_System.Ecommerce.Application.InterfacesServices
{
    public interface IPaymentService
    {
        Task<Payment> CreateAsync(PaymnetDto payment);
        Task<Payment> UpdateAsync(PaymnetDto payment);
        Task DeleteAsync(int paymentId);
        Task<Payment> GetPaymentByIdAsync(int PaymentId);
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment> GetUserPaymentAsync(int orderId);
    }
}
