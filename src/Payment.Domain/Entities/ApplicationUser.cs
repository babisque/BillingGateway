using Microsoft.AspNetCore.Identity;

namespace Payment.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}