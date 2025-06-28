using BillingGateway.Application.Handlers.Subscription.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BillingGateway.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionCommand command)
    {
        var res = await mediator.Send(command);
        if (!res.IsSuccess)
            return BadRequest(new { Message = "Operation failed", Errors = res.Error.Description });

        return Created("api/subscription", new { Message = "Subscription created successfully" });
    }
}