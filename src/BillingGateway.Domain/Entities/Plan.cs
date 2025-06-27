namespace BillingGateway.Domain.Entities;

public class Plan
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public enum BillingCycle
    {
        Monthly = 1,
        Yearly = 2,
    }
    public bool IsActive { get; set; } = true;
}