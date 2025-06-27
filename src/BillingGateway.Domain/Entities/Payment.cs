using BillingGateway.Domain.Enums;

namespace BillingGateway.Domain.Entities;

public class Payment
{
    public Guid Id { get; set; }
    public Guid SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; }
    public string PaymentGatewayTransactionId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}