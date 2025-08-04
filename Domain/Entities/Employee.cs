namespace Domain.Entities;

public class Employee
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Postcode { get; set; }
    public string? Email { get; set; }
    public DateTime RecordDate { get; set; }
}
