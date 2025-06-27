using BillingGateway.Domain.Entities;
using BillingGateway.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BillingGateway.Infrastructure.Repositories;

public class CustomerRepository(ApplicationDbContext context) : Repository<Customer>(context), ICustomerRepository
{
    public async Task<Customer?> FindByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));

        return await _dbSet.FirstOrDefaultAsync(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }
}