using AutoMapper;
using BillingGateway.Application.Contracts;
using BillingGateway.Domain.Interfaces;
using BillingGateway.Domain.Shared;
using MassTransit;
using MediatR;

namespace BillingGateway.Application.Handlers.Subscription.Create;

public class CreateSubscriptionHandler(
    ICustomerRepository customerRepository,
    IPlanRepository planRepository,
    IMapper mapper,
    ISubscriptionRepository subscriptionRepository,
    IPublishEndpoint publishEndpoint)
    : IRequestHandler<CreateSubscriptionCommand, Result>
{
    public async Task<Result> Handle(CreateSubscriptionCommand req, CancellationToken cancellationToken)
    {
        var user = await customerRepository.GetByIdAsync(req.CustomerId);
        if (user is null)
        {
            var error = new Error("CustomerNotFound", "The specified customer does not exist.");
            return Result.Failure(error);
        }

        var plan = await planRepository.GetByIdAsync(req.PlanId);
        if (plan is null || !plan.IsActive)
        {
            var error = new Error("PlanNotFound", "The specified plan does not exist or is not active.");
            return Result.Failure(error);
        }

        var subscription = mapper.Map<Domain.Entities.Subscription>(req);
        await subscriptionRepository.AddAsync(subscription);
        await subscriptionRepository.SaveChangesAsync();

        await publishEndpoint.Publish(new SubscriptionCreated
        {
            SubscriptionId = subscription.Id,
            CustomerId = subscription.CustomerId,
            PlanId = subscription.PlanId,
            Timestamp = DateTime.UtcNow
        }, cancellationToken);

        return Result.Success();
    }
}