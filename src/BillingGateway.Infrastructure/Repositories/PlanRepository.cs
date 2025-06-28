using BillingGateway.Domain.Entities;
using BillingGateway.Domain.Interfaces;

namespace BillingGateway.Infrastructure.Repositories;

public class PlanRepository(ApplicationDbContext context) : Repository<Plan>(context), IPlanRepository { }