using System.Threading.Tasks;
using Dapper;
using System.Data;
using back_end.src.models;

public class CreditRepository
{
    private readonly IDbConnection _dbConnection;

    public CreditRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Credit> FindByIdAsync(int id)
    {
        var query = "SELECT * FROM \"Credits\" WHERE \"Id\" = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<Credit>(query, new { Id = id });
    }

    public async Task<List<Credit>> GetCreditsByUserId(int userId)
    {
        var query = "SELECT * FROM \"Credits\" WHERE \"UserId\" = @UserId";
        return (await _dbConnection.QueryAsync<Credit>(query, new { UserId = userId })).ToList();
    }

    public async Task<List<Credit>> GetAllCreditsByUserId(int userId)
    {
        var query = "SELECT * FROM \"Credits\" WHERE \"UserId\" = @UserId";
        return (await _dbConnection.QueryAsync<Credit>(query, new { UserId = userId })).ToList();
    }

    public async Task StoreCredit(Credit credit)
    {
        var query = "INSERT INTO \"Credits\" (\"UserId\", \"Amount\", \"Description\", \"CreatedAt\") VALUES (@UserId, @Amount, @Description, @CreatedAt)";
        await _dbConnection.ExecuteAsync(query, credit);
    }

    public async Task DeleteCredit(int id)
    {
        var query = "DELETE FROM \"Credits\" WHERE \"Id\" = @Id";
        await _dbConnection.ExecuteAsync(query, new { Id = id });
    }

    public async Task UpdateCredit(Credit credit, int newAmount)
    {
        var query = "UPDATE \"Credits\" SET \"Amount\" = @NewAmount WHERE \"Id\" = @Id";
        await _dbConnection.ExecuteAsync(query, new { NewAmount = newAmount, Id = credit.Id });
    }

    public async Task<List<Credit>> FullPayment(Credit credit)
    {
        var query = "SELECT * FROM \"Credits\" WHERE \"Id\" = @Id";
        return (await _dbConnection.QueryAsync<Credit>(query, new { Id = credit.Id })).ToList();
    }
}