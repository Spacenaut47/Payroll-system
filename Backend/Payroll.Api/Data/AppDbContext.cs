using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Payroll.Api.Domain.Entities;

namespace Payroll.Api.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<PayrollRun> PayrollRuns => Set<PayrollRun>();
    public DbSet<Payslip> Payslips => Set<Payslip>();
    public DbSet<TaxRule> TaxRules => Set<TaxRule>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
