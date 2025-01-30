namespace WebApplication2.Application.Entities;

public class Account
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
    public string FullName { get; set; }
    public string Pin { get; set; }
    
    public string? Email { get; set; } 
    public decimal Balance { get; set; }
}