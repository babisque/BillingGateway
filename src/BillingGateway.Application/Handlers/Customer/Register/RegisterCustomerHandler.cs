using AutoMapper;
using BillingGateway.Domain.Interfaces;
using BillingGateway.Domain.Shared;
using MediatR;

namespace BillingGateway.Application.Handlers.Customer.Register;

public class RegisterCustomerHandler(IMapper mapper, ICustomerRepository customerRepository) 
    : IRequestHandler<RegisterCustomerCommand, Result>
{
    public async Task<Result> Handle(RegisterCustomerCommand req, CancellationToken cancellationToken)
    {
        try
        {
            var userExists = await customerRepository.FindByEmailAsync(req.Email);
            if (userExists is not null)
            {
                var error = new Error("UserAlreadyExists", "A user with this email already exists");
                return Result.Failure<Domain.Entities.Customer>(error);
            }

            var user = mapper.Map<Domain.Entities.Customer>(req);
            await customerRepository.AddAsync(user);

            return Result.Success();
        }
        catch (Exception e)
        {
            var error = new Error("UserCreationFailed", e.Message);
            return Result.Failure<Domain.Entities.Customer>(error);
        }
    }
}