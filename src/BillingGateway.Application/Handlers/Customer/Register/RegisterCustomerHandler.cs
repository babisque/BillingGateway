using AutoMapper;
using BillingGateway.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BillingGateway.Application.Handlers.Customer.Register;

public class RegisterCustomerHandler(IMapper mapper, UserManager<Domain.Entities.Customer> userManager) 
    : IRequestHandler<RegisterCustomerCommand, Result>
{
    public async Task<Result> Handle(RegisterCustomerCommand req, CancellationToken cancellationToken)
    {
        var userExists = await userManager.FindByEmailAsync(req.Email);
        if (userExists is not null)
        {
            var error = new Error("UserAlreadyExists", "A user with this email already exists");
            return Result.Failure<Domain.Entities.Customer>(error);
        }
        
        var user = mapper.Map<Domain.Entities.Customer>(req);
        var result = await userManager.CreateAsync(user, req.Password);

        if (!result.Succeeded)
        {
            var error = new Error("UserCreationFailed", "User creation failed");
            return Result.Failure<Domain.Entities.Customer>(error);
        }

        return Result.Success();
    }
}