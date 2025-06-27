using AutoMapper;
using BillingGateway.Application.Handlers.Customer;
using BillingGateway.Application.Handlers.Customer.Register;
using BillingGateway.Domain.Entities;

namespace BillingGateway.Application.Profiles;

public class CustomerMapper : Profile
{
    public CustomerMapper()
    {
        CreateMap<RegisterCustomerCommand, Customer>()
            .ReverseMap();
    }
}