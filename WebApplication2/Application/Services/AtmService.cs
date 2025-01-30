using WebApplication2.Application.Entities;
using WebApplication2.Application.Interfaces;
namespace WebApplication2.Application.Services;

public class AtmService
{
    private readonly IAccountRepository _accountRepository;

    public AtmService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public void Register(string accountNumber, string pin, string fullName)
    {
        // Проверяем, есть ли уже такой аккаунт
        if (_accountRepository.GetAccountByNumber(accountNumber) != null)
        {
            throw new Exception("Пользователь с таким номером счета уже существует.");
        }

        // Создаем новый аккаунт
        var user = new Account()
        {
            AccountNumber = accountNumber,
            Pin = pin,
            FullName = fullName,
            Balance = 0
        };

        _accountRepository.CreateAccount(user);
    }
    
    public Account Login(string accountNumber, string pin)
    {
        var account = _accountRepository.GetAccountByNumber(accountNumber);
        if (account == null || account.Pin != pin)
        {
            throw new Exception("Неверный номер счета или пин-код.");
        }
        return account;
    }

    public void Deposit(Account account, decimal amount)
    {
        if (amount <= 0)
        {
            throw new Exception("Сумма должна быть положительной.");
        }

        account.Balance += amount;
        _accountRepository.UpdateAccount(account);

        _accountRepository.AddTransaction(new Transaction
        {
            AccountId = account.Id,
            Type = "Deposit",
            Amount = amount,
            Timestamp = DateTime.Now
        });
    }

    public void Withdraw(Account account, decimal amount)
    {
        if (amount <= 0)
        {
            throw new Exception("Сумма должна быть положительной.");
        }

        if (account.Balance < amount)
        {
            throw new Exception("Недостаточно средств на счете.");
        }

        account.Balance -= amount;
        _accountRepository.UpdateAccount(account);

        _accountRepository.AddTransaction(new Transaction
        {
            AccountId = account.Id,
            Type = "Withdraw",
            Amount = amount,
            Timestamp = DateTime.Now
        });
    }

    public IEnumerable<Transaction> GetTransactionHistory(Account account)
    {
        return _accountRepository.GetTransactionsByAccountId(account.Id);
    }
    
    public decimal GetBalance(Account account)
    {
        return account.Balance;
    }

}