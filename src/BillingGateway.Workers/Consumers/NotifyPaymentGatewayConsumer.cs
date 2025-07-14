using System.Threading.Tasks;

namespace BillingGateway.Workers.Consumers
{
    // TODO: create a webhook to notify the payment gateway about the new subscription
    public class NotifyPaymentGatewayConsumer
    {
        public Task ConsumeAsync(object subscription)
        {
            // TODO: Implement logic to send a webhook notification to the payment gateway
            // Example: Use an injected IWebhookService to send eventType: "SubscriptionCreatedForPaymentGateway"
            return Task.CompletedTask;
        }
    }
}

