namespace WebApplication2.Presentation.Models;

public class UserRegisterRequest
{
    public string AccountNumber { get; set; }
    public string Pin { get; set; }
    public string FullName { get; set; }
    public string? Email { get; set; }
}
