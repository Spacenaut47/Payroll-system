namespace Payroll.Api.Domain.DTOs;

public record EmployeeCreateDto(
    string EmpCode, string FirstName, string LastName, string Email,
    string Department, DateTime DateOfJoining, decimal BaseSalary,
    decimal HraPercent, decimal OtherAllowance, decimal PfPercent
);

public record EmployeeReadDto(
    int Id, string EmpCode, string FirstName, string LastName, string Email,
    string Department, DateTime DateOfJoining, decimal BaseSalary,
    decimal HraPercent, decimal OtherAllowance, decimal PfPercent, bool IsActive
);
