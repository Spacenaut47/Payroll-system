namespace Payroll.Api.Domain.Entities;

public class PayrollRun
{
    public int Id { get; set; }
    public DateOnly PeriodStart { get; set; }
    public DateOnly PeriodEnd { get; set; }
    public DateTime RunAtUtc { get; set; } = DateTime.UtcNow;
    public string RunByUserId { get; set; } = default!;
    public ICollection<Payslip> Payslips { get; set; } = new List<Payslip>();
}
