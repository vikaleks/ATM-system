namespace WebApplication2.Application.Entities;

public class Transaction
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }
}