using BillingGateway.Domain.Entities;
using BillingGateway.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BillingGateway.Infrastructure.Repositories;

public class PaymentRepository(ApplicationDbContext context) 
  : Repository<Payment>(context), IPaymentRepository { }
