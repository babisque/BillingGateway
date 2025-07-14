using BillingGateway.API.Controllers;
using BillingGateway.Application.Handlers.Customer.Register;
using BillingGateway.Domain.Entities;
using BillingGateway.Domain.Shared;
using BillingGateway.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BillingGateway.Tests.API.Controllers;

public class CustomerControllerIntegrationTests
{
    private readonly CustomerController _controller;
    private readonly ApplicationDbContext _dbContext;
    private readonly Mock<IMediator> _mediatorMock;

    public CustomerControllerIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _dbContext = new ApplicationDbContext(options);
        _mediatorMock = new Mock<IMediator>();
        _controller = new CustomerController(_mediatorMock.Object);
    }

    [Fact]
    public async Task RegisterCustomer_ShouldSaveToDatabase_WhenDataIsValid()
    {
        // Arrange
        var command = new RegisterCustomerCommand { FullName = "John Doe", Email = "johndoe@email.com" };
        var customer = new Customer
            { FullName = command.FullName, Email = command.Email, PaymentGatewayCustomerId = "TestGatewayId" };
        _mediatorMock.Setup(m => m.Send(command, default))
            .Callback(() =>
            {
                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();
            })
            .ReturnsAsync(Result.Success());

        // Act
        var response = await _controller.RegisterCustomer(command);

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(response);
        Assert.Equal("Customer registered successfully",
            createdResult.Value?.GetType().GetProperty("Message")?.GetValue(createdResult.Value));

        var savedCustomer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Email == command.Email);
        Assert.NotNull(savedCustomer);
        Assert.Equal(command.FullName, savedCustomer.FullName);
        Assert.Equal(command.Email, savedCustomer.Email);
    }
}