namespace BillingGateway.Application.Contracts;

public record NotifyPlanOwner
{
    public Guid SubscriptionId { get; init; }
    public Guid PlanId { get; init; }
}