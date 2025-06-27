using MediatR;
using Microsoft.AspNetCore.Mvc;
using BillingGateway.Application.Handlers.Customer;

namespace BillingGateway.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerCommand command)
    {
        var res = await mediator.Send(command);
        if (!res.IsSuccess)
            return BadRequest(new { Message = "Operation failed", Errors = res.Error.Description });
        
        return Created("api/customer", new { Message = "Customer registered successfully" });
        
    }
}