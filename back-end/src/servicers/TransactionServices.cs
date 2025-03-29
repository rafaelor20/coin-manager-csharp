using System;
using System.Threading.Tasks;

using back_end.src.models;

public class TransactionService(TransactionRepository transactionRepository)
{
    private readonly TransactionRepository _transactionRepository = transactionRepository;
    private readonly UserRepository userRepository;

    async Task CheckUserById(int id)
    {
        var user = await userRepository.FindByIdAsync(id);
        if (user == null)
        {
            throw new Exception("UnauthorizedError");
        }
    }

    async Task CheckTransactionById(int id)
    {
        var transaction = await _transactionRepository.FindByIdAsync(id);
        if (transaction == null)
        {
            throw new Exception("InvalidTransactionIdError");
        }
    }

    void CheckAmount(decimal amount)
    {
        if (amount.GetType() != typeof(decimal))
        {
            throw new Exception("InvalidAmountError");
        }
    }

    void IsValidDescription(string description)
    {
        if (description.GetType() != typeof(string))
        {
            throw new Exception("InvalidDescriptionError");
        }
        if (description.Trim() == "")
        {
            throw new Exception("InvalidDescriptionError");
        }
    }


    public async Task<List<Transaction>> GetHistoric()
    {
        try
        {
            var historic = await _transactionRepository.getHistoric();
            return historic;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error: {ex.Message}");
        }
    }

    public async Task<Transaction> StoreTransaction(string Description, decimal Amount, string Entity, DateTime CreatedAt)
    {
        try
        {
            IsValidDescription(Description);
            CheckAmount(Amount);
            var transaction = await _transactionRepository.storeTransaction(Description, Amount, Entity, CreatedAt);
            return transaction;

        }
        catch (Exception ex)
        {
            throw new Exception($"Error: {ex.Message}");
        }
    }

    public async Task<Transaction> DeleteTransaction(int transactionId)
    {
        try
        {
            await _transactionRepository.deleteTransaction(transactionId);
            return new Transaction
            {
                Description = "Deleted",
                Amount = 0,
                Entity = "Deleted",
                CreatedAt = DateTime.Now,
                User = null // Set this to an appropriate value, e.g., a default user or null if allowed
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error: {ex.Message}");
        }
    }
}

