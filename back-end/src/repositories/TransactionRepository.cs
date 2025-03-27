using System.Threading.Tasks;
using Dapper;
using System.Data;

public class TransactionRepository
{
    private readonly IDbConnection _dbConnection;

    public TransactionRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<List<Transaction>> getHistoric()
    {
        var query = "SELECT * FROM \"Transactions\"";
        return (await _dbConnection.QueryAsync<Transaction>(query)).ToList();
    }

    public async Task<Transaction> storeTransaction(string description, decimal amount, string entity, DateTime? date)
    {
        if (!date.HasValue)
        {
            date = DateTime.Now;
        }
        var query = "INSERT INTO \"Transactions\" (\"Description\", \"Amount\", \"Entity\", \"Date\") VALUES (@Description, @Amount, @Entity, @Date) RETURNING *";
        return await _dbConnection.QuerySingleAsync<Transaction>(query, new { Description = description, Amount = amount, Entity = entity, Date = date });
    }

    public async Task deleteTransaction(int transactionId)
    {
        var query = "DELETE FROM \"Transactions\" WHERE \"Id\" = @Id";
        await _dbConnection.ExecuteAsync(query, new { Id = transactionId });
    }

    public async Task<Transaction> FindByIdAsync(int id)
    {
        var query = "SELECT * FROM \"Transactions\" WHERE id = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<Transaction>(query, new { Id = id });
    }
}