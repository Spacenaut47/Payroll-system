using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Api.Data;
using Payroll.Api.Domain.DTOs;
using Payroll.Api.Domain.Entities;

namespace Payroll.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EmployeesController : ControllerBase
{
    private readonly AppDbContext _db;
    public EmployeesController(AppDbContext db) { _db = db; }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> Get()
    {
        var data = await _db.Employees.Select(e => new EmployeeReadDto(
            e.Id, e.EmpCode, e.FirstName, e.LastName, e.Email, e.Department, e.DateOfJoining,
            e.BaseSalary, e.HraPercent, e.OtherAllowance, e.PfPercent, e.IsActive
        )).ToListAsync();
        return Ok(data);
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeReadDto>> Create(EmployeeCreateDto dto)
    {
        var e = new Employee {
            EmpCode = dto.EmpCode, FirstName = dto.FirstName, LastName = dto.LastName,
            Email = dto.Email, Department = dto.Department, DateOfJoining = dto.DateOfJoining,
            BaseSalary = dto.BaseSalary, HraPercent = dto.HraPercent,
            OtherAllowance = dto.OtherAllowance, PfPercent = dto.PfPercent, IsActive = true
        };
        _db.Employees.Add(e);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = e.Id }, e);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeReadDto>> GetById(int id)
    {
        var e = await _db.Employees.FindAsync(id);
        if (e == null) return NotFound();
        return Ok(new EmployeeReadDto(
            e.Id, e.EmpCode, e.FirstName, e.LastName, e.Email, e.Department, e.DateOfJoining,
            e.BaseSalary, e.HraPercent, e.OtherAllowance, e.PfPercent, e.IsActive
        ));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, EmployeeCreateDto dto)
    {
        var e = await _db.Employees.FindAsync(id);
        if (e == null) return NotFound();

        e.EmpCode = dto.EmpCode; e.FirstName = dto.FirstName; e.LastName = dto.LastName;
        e.Email = dto.Email; e.Department = dto.Department; e.DateOfJoining = dto.DateOfJoining;
        e.BaseSalary = dto.BaseSalary; e.HraPercent = dto.HraPercent; e.OtherAllowance = dto.OtherAllowance;
        e.PfPercent = dto.PfPercent;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var e = await _db.Employees.FindAsync(id);
        if (e == null) return NotFound();
        _db.Remove(e);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
