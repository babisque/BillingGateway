namespace BillingGateway.Application.Contracts;

public record NotifyBillingSystem
{
    public Guid SubscriptionId { get; init; }
}