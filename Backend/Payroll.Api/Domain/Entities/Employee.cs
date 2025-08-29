namespace Payroll.Api.Domain.Entities;

public class Employee
{
    public int Id { get; set; }
    public string EmpCode { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Department { get; set; } = default!;
    public DateTime DateOfJoining { get; set; }
    public decimal BaseSalary { get; set; }            // fixed monthly
    public decimal HraPercent { get; set; } = 0.4m;    // default 40% of base
    public decimal OtherAllowance { get; set; } = 0m;
    public decimal PfPercent { get; set; } = 0.12m;    // 12% PF default
    public bool IsActive { get; set; } = true;
}
