using BillingGateway.Application.Contracts;
using BillingGateway.Application.Interfaces.Services;
using BillingGateway.Domain.Interfaces;
using MassTransit;

namespace BillingGateway.Workers.Consumers
{
    public class NotifyCustomerConsumer(IEmailService emailService, ICustomerRepository customerRepository)
        : IConsumer<NotifyCustomer>
    {
        public async Task Consume(ConsumeContext<NotifyCustomer> context)
        {
            var customer = await customerRepository.GetByIdAsync(context.Message.CustomerId);
            if (customer is not null)
            {
                const string subject = "Subscription Created";
                var body =
                    $"Dear {customer.FullName},\n\nYour subscription has been successfully created.\n\nThank you for choosing our service!";
                await emailService.SendEmailAsync(customer.Email, subject, body);
            }
        }
    }
}