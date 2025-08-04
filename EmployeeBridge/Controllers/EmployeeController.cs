using Application.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBridge.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<IActionResult> Index(string search)
    {
        var employees = await _employeeService.GetAllSortedAsync(search);
        return View(employees);
    }

    [HttpPost]
    public async Task<IActionResult> Import(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is empty.");

        var rowCount = await _employeeService.ImportEmployeesFromCsvAsync(file.OpenReadStream());

        TempData["Success"] = $"{rowCount} rows imported successfully.";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Employee employee)
    {
        await _employeeService.UpdateEmployeeAsync(employee);
        return RedirectToAction("Index");
    }
}
