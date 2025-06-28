using AutoMapper;
using BillingGateway.Domain.Interfaces;
using BillingGateway.Domain.Shared;
using MediatR;

namespace BillingGateway.Application.Handlers.Subscription.Create;

public class CreateSubscriptionHandler(ICustomerRepository customerRepository, IPlanRepository planRepository, IMapper mapper, ISubscriptionRepository subscriptionRepository) 
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
        
        // TODO: create a webhook to notify the customer about the new subscription
        // TODO: create a webhook to notify the plan owner about the new subscription
        // TODO: create a webhook to notify the billing system about the new subscription
        // TODO: create a webhook to notify the payment gateway about the new subscription
        // TODO: create a webhook to realize the subscription in the payment gateway
        
        return Result.Success();
    }
}