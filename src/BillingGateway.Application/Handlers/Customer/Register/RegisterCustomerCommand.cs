using System.ComponentModel.DataAnnotations;
using BillingGateway.Domain.Shared;
using MediatR;

namespace BillingGateway.Application.Handlers.Customer.Register;

public class RegisterCustomerCommand : IRequest<Result>
{
    [Required]
    public required string FullName { get; set; }
    [Required, EmailAddress]
    public required string Email { get; set; }
}