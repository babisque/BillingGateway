using BillingGateway.Application.Contracts;
using MassTransit;

namespace BillingGateway.Workers.Consumers;

public class SubscriptionCreatedConsumer(ILogger<SubscriptionCreatedConsumer> logger, IPublishEndpoint publishEndpoint)
    : IConsumer<SubscriptionCreated>
{
    public async Task Consume(ConsumeContext<SubscriptionCreated> context)
    {
        logger.LogInformation("Subscription created event received: {SubscriptionId}", context.Message.SubscriptionId);

        await publishEndpoint.Publish(new NotifyCustomer { CustomerId = context.Message.CustomerId, SubscriptionId = context.Message.SubscriptionId });
        await publishEndpoint.Publish(new NotifyPlanOwner { PlanId = context.Message.PlanId, SubscriptionId = context.Message.SubscriptionId });
        await publishEndpoint.Publish(new NotifyBillingSystem { SubscriptionId = context.Message.SubscriptionId });
        await publishEndpoint.Publish(new NotifyPaymentGateway { SubscriptionId = context.Message.SubscriptionId });
        await publishEndpoint.Publish(new RealizeSubscriptionInPaymentGateway { SubscriptionId = context.Message.SubscriptionId, CustomerId = context.Message.CustomerId, PlanId = context.Message.PlanId });
        
        logger.LogInformation("Fan-out events published for subscription: {SubscriptionId}", context.Message.SubscriptionId);
    }
}