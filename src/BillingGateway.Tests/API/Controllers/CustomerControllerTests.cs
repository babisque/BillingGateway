using Xunit;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BillingGateway.API.Controllers;
using BillingGateway.Application.Handlers.Customer.Register;
using BillingGateway.Domain.Shared;

namespace BillingGateway.Tests.API.Controllers;

public class CustomerControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly CustomerController _controller;

    public CustomerControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new CustomerController(_mediatorMock.Object);
    }

    [Fact]
    public async Task RegisterCustomer_ShouldReturnCreated_WhenOperationIsSuccessful()
    {
        // Arrange
        var command = new RegisterCustomerCommand
        {
            FullName = "John Doe",
            Email = "johndoe@email.com"
        };
        var result = Result.Success();
        _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

        // Act
        var response = await _controller.RegisterCustomer(command);

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(response);
        Assert.Equal("Customer registered successfully", createdResult.Value?.GetType().GetProperty("Message")?.GetValue(createdResult.Value));
    }

    [Fact]
    public async Task RegisterCustomer_ShouldReturnBadRequest_WhenOperationFails()
    {
        // Arrange
        var command = new RegisterCustomerCommand
        {
            FullName = "John Doe",
            Email = "johndoe@email.com"
        };
        var result = Result.Failure(new Error("ErrorCode", "ErrorDescription"));
        _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

        // Act
        var response = await _controller.RegisterCustomer(command);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(response);
        Assert.Equal("Operation failed", badRequestResult.Value?.GetType().GetProperty("Message")?.GetValue(badRequestResult.Value));
        Assert.Equal("ErrorDescription", badRequestResult.Value?.GetType().GetProperty("Errors")?.GetValue(badRequestResult.Value));
    }
}
