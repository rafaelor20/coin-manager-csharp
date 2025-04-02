using System.Threading.Tasks;
using Dapper;
using System.Data;
using back_end.src.models;

public class DebtRepository
{
    private readonly IDbConnection _dbConnection;

    public DebtRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Debt> FindByIdAsync(int id)
    {
        var query = "SELECT * FROM \"Debts\" WHERE \"Id\" = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<Debt>(query, new { Id = id });
    }

    public async Task<List<Debt>> GetDebtsByUserId(int userId)
    {
        var query = "SELECT * FROM \"Debts\" WHERE \"UserId\" = @UserId";
        return (await _dbConnection.QueryAsync<Debt>(query, new { UserId = userId })).ToList();
    }

    public async Task<List<Debt>> GetAllDebtsByUserId(int userId)
    {
        var query = "SELECT * FROM \"Debts\" WHERE \"UserId\" = @UserId";
        return (await _dbConnection.QueryAsync<Debt>(query, new { UserId = userId })).ToList();
    }

    public async Task<Debt> CreateAsync(Debt debt)
    {
        var query = "INSERT INTO \"Debts\" (\"UserId\", \"Description\", \"Creditor\", \"Amount\", \"CreatedAt\", \"Paid\") " +
                    "VALUES (@UserId, @Description, @Creditor, @Amount, @CreatedAt, @Paid) RETURNING *";
        return await _dbConnection.QueryFirstOrDefaultAsync<Debt>(query, debt);
    }

    public async Task<Debt> DeleteAsync(int id)
    {
        var query = "DELETE FROM \"Debts\" WHERE \"Id\" = @Id RETURNING *";
        return await _dbConnection.QueryFirstOrDefaultAsync<Debt>(query, new { Id = id });
    }

    public async Task<Debt> UpdateAsync(Debt debt, decimal newAmount)
    {
        debt.Amount = newAmount;
        var query = "UPDATE \"Debts\" SET \"Description\" = @Description, \"Creditor\" = @Creditor, \"Amount\" = @Amount, " +
                    "\"CreatedAt\" = @CreatedAt, \"Paid\" = @Paid WHERE \"Id\" = @Id RETURNING *";
        return await _dbConnection.QueryFirstOrDefaultAsync<Debt>(query, debt);
    }

    public async Task<Debt> payDebtAsync(int id)
    {
        var query = "UPDATE \"Debts\" SET \"Paid\" = true, \"PayDate\" = @PayDate WHERE \"Id\" = @Id RETURNING *";
        var parameters = new { Id = id, PayDate = DateTime.UtcNow };
        return await _dbConnection.QueryFirstOrDefaultAsync<Debt>(query, parameters);
    }
}


