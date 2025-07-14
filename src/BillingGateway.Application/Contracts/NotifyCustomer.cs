namespace BillingGateway.Application.Contracts;

public record NotifyCustomer
{
    public Guid SubscriptionId { get; init; }
    public Guid CustomerId { get; init; }
}