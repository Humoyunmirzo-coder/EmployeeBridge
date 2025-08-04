using Application.IServices;
using CsvHelper;
using Domain.Dto;
using Domain.Entities;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Infrastructure.Services;

public class EmployeeService(MyDbContext context) : IEmployeeService
{
    private readonly MyDbContext _ctx = context;

    public async Task<int> ImportEmployeesFromCsvAsync(Stream csvStream)
    {
        using var reader = new StreamReader(csvStream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<EmployeeCsvDto>().ToList();

        var employees = records.Select(r => new Employee
        {
            Code = r.Code,
            FirstName = r.FirstName,
            LastName = r.LastName,
           DateOfBirth=DateTime.SpecifyKind(r.DateOfBirth, DateTimeKind.Utc),
            Phone = r.Phone,
            Mobile = r.Mobile,
            Address = r.Address,
            City = r.City,
            Postcode = r.Postcode,
            Email = r.Email,
            RecordDate = DateTime.SpecifyKind(r.RecordDate, DateTimeKind.Utc)
        }).ToList();

        await _ctx.Employees.AddRangeAsync(employees);
        return await _ctx.SaveChangesAsync();
    }

    public async Task<List<Employee>> GetAllSortedAsync(string search = null!)
    {
        var query = _ctx.Employees.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(e => e.FirstName!.Contains(search) || e.LastName!.Contains(search));

        return await query.OrderBy(e => e.LastName).ToListAsync();
    }

    public async Task UpdateEmployeeAsync(Employee employee)
    {
        _ctx.Employees.Update(employee);
        await _ctx.SaveChangesAsync();
    }
}
