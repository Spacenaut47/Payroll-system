using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Api.Data;
using Payroll.Api.Domain.DTOs;
using Payroll.Api.Services;

namespace Payroll.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PayrollController : ControllerBase
{
    private readonly PayrollService _payrollService;
    private readonly AppDbContext _db;
    private readonly IHttpContextAccessor _ctx;

    public PayrollController(PayrollService svc, AppDbContext db, IHttpContextAccessor ctx)
    {
        _payrollService = svc; _db = db; _ctx = ctx;
    }

    [HttpPost("run")]
    public async Task<ActionResult> Run(PayrollRunRequest req)
    {
        var userId = _ctx.HttpContext?.User?.Identity?.Name ?? "system";
        var run = await _payrollService.RunPayrollAsync(req.PeriodStart, req.PeriodEnd, req.DefaultOvertime, req.DefaultUnpaidLeave, userId);
        return Ok(new { run.Id, run.PeriodStart, run.PeriodEnd, Payslips = run.Payslips.Count });
    }

    [HttpGet("runs")]
    public async Task<ActionResult> Runs()
    {
        var runs = await _db.PayrollRuns
            .Include(r => r.Payslips).ThenInclude(p => p.Employee)
            .OrderByDescending(r => r.RunAtUtc)
            .Take(20)
            .ToListAsync();

        var dto = runs.Select(r => new {
            r.Id, r.PeriodStart, r.PeriodEnd, r.RunAtUtc,
            Payslips = r.Payslips.Select(p => new {
                p.Id, p.EmployeeId, EmployeeName = p.Employee.FirstName + " " + p.Employee.LastName,
                p.Gross, p.Net, p.Tax, p.Pf
            })
        });

        return Ok(dto);
    }
}
