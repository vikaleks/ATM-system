using Microsoft.Data.Sqlite;
using WebApplication2.Application;
using WebApplication2.Application.Entities;
using WebApplication2.Application.Interfaces;

namespace WebApplication2.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private const string ConnectionString = "...";

    public void CreateAccount(Account account)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
            INSERT INTO Accounts (AccountNumber, Pin, Balance)
            VALUES (@accountNumber, @pin, @balance)";
            command.Parameters.AddWithValue("@accountNumber", account.AccountNumber);
            command.Parameters.AddWithValue("@pin", account.Pin);
            command.Parameters.AddWithValue("@balance", account.Balance);
            command.ExecuteNonQuery();
        }
    }
    
    public Account GetAccountByNumber(string accountNumber)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Accounts WHERE AccountNumber = @accountNumber";
            command.Parameters.AddWithValue("@accountNumber", accountNumber);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Account {
                        Id = reader.GetInt32(0),
                        AccountNumber = reader.GetString(1),
                        Pin = reader.GetString(2),
                        Balance = reader.GetDecimal(3)
                    };
                }
            }
        }
        return null;
    }
    
    public void UpdateAccount(Account account)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Accounts SET Balance = @balance WHERE Id = @id";
            command.Parameters.AddWithValue("@balance", account.Balance);
            command.Parameters.AddWithValue("@id", account.Id);
            command.ExecuteNonQuery();
        }
    }
    
    public void AddTransaction(Transaction transaction)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Transactions (AccountId, Type, Amount, Timestamp)
                VALUES (@accountId, @type, @amount, @timestamp)";
            command.Parameters.AddWithValue("@accountId", transaction.AccountId);
            command.Parameters.AddWithValue("@type", transaction.Type);
            command.Parameters.AddWithValue("@amount", transaction.Amount);
            command.Parameters.AddWithValue("@timestamp", transaction.Timestamp);
            command.ExecuteNonQuery();
        }
    }
    public IEnumerable<Transaction> GetTransactionsByAccountId(int accountId)
    {
        var transactions = new List<Transaction>();
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Transactions WHERE AccountId = @accountId";
            command.Parameters.AddWithValue("@accountId", accountId);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    transactions.Add(new Transaction
                    {
                        Id = reader.GetInt32(0),
                        AccountId = reader.GetInt32(1),
                        Type = reader.GetString(2),
                        Amount = reader.GetDecimal(3),
                        Timestamp = reader.GetDateTime(4)
                    });
                }
            }
        }
        return transactions;
    }
}
