namespace WebApplication2.Presentation.Models;

public class WithdrawRequest
{
    public string AccountNumber { get; set; }
    public string Pin { get; set; }
    public decimal Amount { get; set; }
}