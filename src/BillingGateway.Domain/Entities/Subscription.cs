namespace BillingGateway.Domain.Entities;

public class Subscription
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid PlanId { get; set; }

    public enum Status
    {
        Active = 1,
        Canceled = 2,
        PastDue = 3,
    }
    public DateTime StartDate { get; set; }
    public DateTime NextBillingDate { get; set; }
    public DateTime? CancellationDate { get; set; }
}