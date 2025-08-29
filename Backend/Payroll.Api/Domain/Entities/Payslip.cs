namespace Payroll.Api.Domain.Entities;

public class Payslip
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = default!;
    public int PayrollRunId { get; set; }
    public PayrollRun PayrollRun { get; set; } = default!;

    public decimal BaseSalary { get; set; }
    public decimal Hra { get; set; }
    public decimal Allowance { get; set; }
    public decimal OvertimePay { get; set; }
    public decimal UnpaidLeaveDeduction { get; set; }
    public decimal Pf { get; set; }
    public decimal Tax { get; set; }
    public decimal Gross { get; set; }
    public decimal Net { get; set; }
}
