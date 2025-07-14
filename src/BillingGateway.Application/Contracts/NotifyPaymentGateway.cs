namespace BillingGateway.Application.Contracts;

public record NotifyPaymentGateway
{
    public Guid SubscriptionId { get; init; }
}