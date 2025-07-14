using System.Threading.Tasks;

namespace BillingGateway.Workers.Consumers
{
    // TODO: create a webhook to notify the billing system about the new subscription
    public class NotifyBillingSystemConsumer
    {
        public Task ConsumeAsync(object subscription)
        {
            // TODO: Implement logic to send a webhook notification to the billing system
            // Example: Use an injected IWebhookService to send eventType: "SubscriptionCreatedForBillingSystem"
            return Task.CompletedTask;
        }
    }
}

