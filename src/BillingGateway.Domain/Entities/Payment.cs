namespace BillingGateway.Domain.Entities;

public class Payment
{
    public Guid Id { get; set; }
    public Guid SubscriptionId { get; set; }
    public decimal Amount { get; set; }
    public enum Status
    {
        Succeeded = 1,
        Failed = 2,
    }

    public string PaymentGatewayTransactionId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}