using BillingGateway.Domain.Enums;

namespace BillingGateway.Domain.Entities;

public class Plan
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public BillingCycle BillingCycle { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}