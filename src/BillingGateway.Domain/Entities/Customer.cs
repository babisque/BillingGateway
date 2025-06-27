using Microsoft.AspNetCore.Identity;

namespace BillingGateway.Domain.Entities;

public class Customer : IdentityUser
{
    public string FullName { get; set; }
    public string PaymentGatewayCustomerId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}