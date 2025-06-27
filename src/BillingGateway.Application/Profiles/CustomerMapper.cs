using AutoMapper;
using BillingGateway.Application.Handlers.Customer;
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