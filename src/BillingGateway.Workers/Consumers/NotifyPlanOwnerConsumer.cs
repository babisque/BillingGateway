using System.Threading.Tasks;

namespace BillingGateway.Workers.Consumers
{
    // TODO: create a webhook to notify the plan owner about the new subscription
    public class NotifyPlanOwnerConsumer
    {
        public Task ConsumeAsync(object subscription)
        {
            // TODO: Implement logic to send a webhook notification to the plan owner
            // Example: Use an injected IWebhookService to send eventType: "SubscriptionCreatedForPlanOwner"
            return Task.CompletedTask;
        }
    }
}

