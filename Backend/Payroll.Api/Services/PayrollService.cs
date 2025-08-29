using Microsoft.EntityFrameworkCore;
using Payroll.Api.Data;
using Payroll.Api.Domain.Entities;

namespace Payroll.Api.Services;

public class PayrollService
{
    private readonly AppDbContext _db;
    public PayrollService(AppDbContext db) => _db = db;

    public async Task<PayrollRun> RunPayrollAsync(DateOnly start, DateOnly end, decimal defaultOvertime, decimal defaultUnpaidLeave, string runByUserId)
    {
        var employees = await _db.Employees.Where(e => e.IsActive).ToListAsync();
        var rules = await _db.TaxRules.OrderBy(r => r.SlabFrom).ToListAsync();

        var run = new PayrollRun { PeriodStart = start, PeriodEnd = end, RunByUserId = runByUserId };

        foreach (var e in employees)
        {
            var hra = e.BaseSalary * e.HraPercent;
            var gross = e.BaseSalary + hra + e.OtherAllowance + defaultOvertime - defaultUnpaidLeave;

            var yearly = gross * 12;
            var rate = rules.LastOrDefault(r => yearly >= r.SlabFrom && yearly < r.SlabTo)?.Rate ?? 0m;
            var monthlyTax = gross * rate;

            var pf = e.BaseSalary * e.PfPercent;
            var net = gross - (monthlyTax + pf);

            run.Payslips.Add(new Payslip {
                EmployeeId = e.Id,
                BaseSalary = e.BaseSalary,
                Hra = hra,
                Allowance = e.OtherAllowance,
                OvertimePay = defaultOvertime,
                UnpaidLeaveDeduction = defaultUnpaidLeave,
                Pf = pf,
                Tax = monthlyTax,
                Gross = gross,
                Net = net
            });
        }

        _db.PayrollRuns.Add(run);
        await _db.SaveChangesAsync();
        return run;
    }
}
