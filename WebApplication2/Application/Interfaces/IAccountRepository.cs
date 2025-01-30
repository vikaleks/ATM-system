using WebApplication2.Application.Entities;

namespace WebApplication2.Application.Interfaces;

public interface IAccountRepository
{
    Account GetAccountByNumber(string accountNumber);
    void CreateAccount(Account account);
    void UpdateAccount(Account account);
    void AddTransaction(Transaction transaction);
    IEnumerable<Transaction> GetTransactionsByAccountId(int accountId);
}
