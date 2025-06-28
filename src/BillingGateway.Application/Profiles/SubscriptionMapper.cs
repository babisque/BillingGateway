using AutoMapper;
using BillingGateway.Application.Handlers.Subscription.Create;
using BillingGateway.Domain.Entities;

namespace BillingGateway.Application.Profiles;

public class SubscriptionMapper : Profile
{
    public SubscriptionMapper()
    {
        CreateMap<CreateSubscriptionCommand, Subscription>()
            .ReverseMap();
    }   
}