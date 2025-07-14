namespace BillingGateway.Application.Contracts;

public record RealizeSubscriptionInPaymentGateway
{
    public Guid SubscriptionId { get; init; }
    public Guid CustomerId { get; init; }
    public Guid PlanId { get; init; }
}