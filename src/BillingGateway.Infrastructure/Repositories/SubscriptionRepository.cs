using BillingGateway.Domain.Entities;
using BillingGateway.Domain.Interfaces;

namespace BillingGateway.Infrastructure.Repositories;

public class SubscriptionRepository(ApplicationDbContext context) 
    : Repository<Subscription>(context), ISubscriptionRepository { }