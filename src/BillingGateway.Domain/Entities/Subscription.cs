using BillingGateway.Domain.Enums;

namespace BillingGateway.Domain.Entities;

public class Subscription
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
    public Guid PlanId { get; set; }
    public Plan Plan { get; set; }
    public SubscriptionStatus Status { get; set; }
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public DateTime StartDate { get; set; }
    public DateTime NextBillingDate { get; set; }
    public DateTime? CancellationDate { get; set; }
}