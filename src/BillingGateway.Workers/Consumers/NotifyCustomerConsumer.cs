using System.Threading.Tasks;

namespace BillingGateway.Workers.Consumers
{
    // TODO: create a webhook to notify the customer about the new subscription
    public class NotifyCustomerConsumer
    {
        public Task ConsumeAsync(object subscription)
        {
            // TODO: Implement logic to send a webhook notification to the customer
            // Example: Use an injected IWebhookService to send eventType: "SubscriptionCreatedForCustomer"
            return Task.CompletedTask;
        }
    }
}

