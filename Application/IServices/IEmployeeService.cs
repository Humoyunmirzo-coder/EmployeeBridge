using Domain.Entities;

namespace Application.IServices;

public interface IEmployeeService
{
    Task<int> ImportEmployeesFromCsvAsync(Stream csvStream);
    Task<List<Employee>> GetAllSortedAsync(string search = null!);
    Task UpdateEmployeeAsync(Employee employee);
}
