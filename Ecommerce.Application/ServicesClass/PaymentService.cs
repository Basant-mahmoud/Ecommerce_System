using Ecommerce_System.Ecommerce.Application.InterfacesServices;
using Ecommerce_System.Ecommerce.Domain.DTO;
using Ecommerce_System.Ecommerce.Domain.InterfacesRepo;
using Ecommerce_System.Ecommerce.Domain.Models;

namespace Ecommerce_System.Ecommerce.Application.ServicesClass
    {
        public class PaymentService : IPaymentService
        {
            private readonly IPaymentRepository _paymentRepository;
            private readonly IOrderRepository _orderRepository;
            public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository)
            {
                _paymentRepository = paymentRepository;
                _orderRepository = orderRepository;
            }

            public async Task<Payment> CreateAsync(PaymnetDto payment)
            {
                var order = await _orderRepository.GetOrderByIdAsync(payment.OrderId);
                if (order == null)
                {
                    throw new Exception("Order ID Not Found");
                }
                var created = new Payment
                {
                    OrderId = payment.OrderId,
                    Amount = order.TotalAmount,
                    PaymentMethod = payment.PaymentMethod,
                    PaymentDate = payment.PaymentDate,
                };
                var result = await _paymentRepository.CreateAsync(created);
                return result;
            }

            public async Task DeleteAsync(int paymentId)
            {
                var payment = await _paymentRepository.GetPaymentByIdAsync(paymentId);
                if (payment == null)
                {
                    throw new Exception("Payment ID Not Correct");
                }
                var result = await _paymentRepository.DeleteAsync(payment);
                if (result == null)
                {
                    throw new Exception("Cant Remove Payment");
                }

            }

            public async Task<IEnumerable<Payment>> GetAllPaymentAsync()
            {
                var result = await _paymentRepository.GetAllPaymentAsync();
                if (result == null || !result.Any())
                {
                    throw new Exception("No Payment available");
                }
                return result;
            }

            public async Task<Payment> GetPaymentByIdAsync(int PaymentId)
            {
                var payment = await _paymentRepository.GetPaymentByIdAsync(PaymentId);
                if (payment == null)
                {
                    throw new Exception("Payment id not correct");
                }
                return payment;
            }

        public async Task<Payment> UpdatePaymentAsync(PaymnetDto paymentDto)
        {
            var Order = await _orderRepository.GetOrderByIdAsync(paymentDto.OrderId);
            var existingPayment = await _paymentRepository.GetOrderPayment(paymentDto.OrderId);
            if (Order == null)
            {
                throw new Exception("order not found.");
            }
            if (existingPayment == null) 
            {
                throw new Exception("Payment Not found");
            }

            existingPayment.PaymentMethod = paymentDto.PaymentMethod;
            existingPayment.PaymentDate = paymentDto.PaymentDate;

            var updatedPayment = await _paymentRepository.UpdateAsync(existingPayment);

            return updatedPayment;
        }

        // Get payment by OrderId
        public async Task<Payment> GetOrderPaymentAsync(int orderId)
        {
            var payment = await _paymentRepository.GetOrderPayment(orderId);
            if (payment == null)
            {
                throw new Exception("Payment not found for the given Order ID.");
            }

            return payment;
        }
    }
 }

