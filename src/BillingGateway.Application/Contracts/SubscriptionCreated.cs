namespace BillingGateway.Application.Contracts;

public record SubscriptionCreated
{
    public Guid SubscriptionId { get; init; }
    public Guid CustomerId { get; init; }
    public Guid PlanId { get; init; }
    public DateTime Timestamp { get; init; }
}