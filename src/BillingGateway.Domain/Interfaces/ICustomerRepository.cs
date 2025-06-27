using BillingGateway.Domain.Entities;

namespace BillingGateway.Domain.Interfaces;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> FindByEmailAsync(string email);
}