using Ecommerce_System.Ecommerce.Domain.DTO;
using Ecommerce_System.Ecommerce.Domain.Models;
using System.Threading.Tasks;

namespace Ecommerce_System.Ecommerce.Application.InterfacesServices
{
    public interface IPaymentService
    {
        Task<Payment> CreateAsync(PaymnetDto payment);
        Task DeleteAsync(int paymentId);
        Task<IEnumerable<Payment>> GetAllPaymentAsync();
        Task<Payment> GetPaymentByIdAsync(int PaymentId);
        Task<Payment> GetOrderPaymentAsync(int OrderId);
        Task<Payment> UpdatePaymentAsync(PaymnetDto payment);

    }
}
