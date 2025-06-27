namespace BillingGateway.Domain.Entities;

public class Customer
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PaymentGatewayCustomerId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}