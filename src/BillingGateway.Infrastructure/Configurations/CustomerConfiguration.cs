using BillingGateway.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
        
namespace BillingGateway.Infrastructure.Configurations;
        
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();
        builder.Property(c => c.FullName)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(255);
        builder.HasIndex(c => c.Email)
            .IsUnique();
        builder.Property(c => c.PaymentGatewayCustomerId)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasIndex(c => c.PaymentGatewayCustomerId)
            .IsUnique();
        builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");
        builder.HasMany(c => c.Subscriptions)
            .WithOne(s => s.Customer)
            .HasForeignKey(s => s.CustomerId);
    }
}