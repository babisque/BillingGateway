using AutoMapper;
using BillingGateway.Application.Handlers.Customer.Register;
using BillingGateway.Domain.Interfaces;
using Moq;

namespace BillingGateway.Tests.Application.Handlers.Customer;

public class RegisterCustomerHandlerTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly RegisterCustomerHandler _handler;

    public RegisterCustomerHandlerTests()
    {
        _mapperMock = new Mock<IMapper>();
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _handler = new RegisterCustomerHandler(_mapperMock.Object, _customerRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenEmailAlreadyExists()
    {
        // Arrange
        var command = new RegisterCustomerCommand { FullName = "John Doe", Email = "johndoe@email.com" };
        _customerRepositoryMock.Setup(repo => repo.FindByEmailAsync(command.Email))
            .ReturnsAsync(new Domain.Entities.Customer());

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("UserAlreadyExists", result.Error.Code);
        Assert.Equal("A user with this email already exists", result.Error.Description);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenDataIsValid()
    {
        // Arrange
        var command = new RegisterCustomerCommand { FullName = "John Doe", Email = "johndoe@email.com" };
        _customerRepositoryMock.Setup(repo => repo.FindByEmailAsync(command.Email))
            .ReturnsAsync((Domain.Entities.Customer)null);
        _mapperMock.Setup(mapper => mapper.Map<Domain.Entities.Customer>(command)).Returns(new Domain.Entities.Customer
            { FullName = command.FullName, Email = command.Email });

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        Assert.True(result.IsSuccess);
    }
}