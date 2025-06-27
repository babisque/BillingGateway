using System.ComponentModel.DataAnnotations;
using MediatR;
using BillingGateway.Domain.Shared;

namespace BillingGateway.Application.Handlers.Customer;

public class RegisterCustomerCommand : IRequest<Result>
{
    [Required]
    public required string Name { get; set; }
    [Required, EmailAddress]
    public required string Email { get; set; }
    [Required, MinLength(6)]
    public required string Password { get; set; }
}