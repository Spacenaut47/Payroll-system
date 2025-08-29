namespace Payroll.Api.Domain.Entities;

public class TaxRule
{
    public int Id { get; set; }
    public decimal SlabFrom { get; set; }  // inclusive yearly income
    public decimal SlabTo { get; set; }    // exclusive
    public decimal Rate { get; set; }      // e.g., 0.05m for 5%
}
