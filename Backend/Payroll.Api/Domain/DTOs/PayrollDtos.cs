namespace Payroll.Api.Domain.DTOs;

public record PayrollRunRequest(DateOnly PeriodStart, DateOnly PeriodEnd, decimal DefaultOvertime = 0m, decimal DefaultUnpaidLeave = 0m);
public record PayslipReadDto(
    int Id, int EmployeeId, string EmployeeName, decimal Gross, decimal Net, decimal Tax, decimal Pf
);
