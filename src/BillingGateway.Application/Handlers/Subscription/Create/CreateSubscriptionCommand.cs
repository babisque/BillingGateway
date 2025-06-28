using System.ComponentModel.DataAnnotations;
using BillingGateway.Domain.Shared;
using MediatR;

namespace BillingGateway.Application.Handlers.Subscription.Create;

public class CreateSubscriptionCommand : IRequest<Result>
{
    [Required]
    public Guid CustomerId { get; set; }
    [Required]
    public Guid PlanId { get; set; }
}