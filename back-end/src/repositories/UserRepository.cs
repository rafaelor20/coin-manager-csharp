using System.Threading.Tasks;
using Dapper;
using System.Data;
using back_end.src.models;
public class UserRepository
{
    private readonly IDbConnection _dbConnection;

    public UserRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;

    }

    public async Task<User> FindByEmailAsync(string email)
    {
        var query = "SELECT * FROM \"Users\" WHERE \"Email\" = @Email";
        return await _dbConnection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
    }

    public async Task<User> FindByEmailAndPasswordAsync(string email, string password)
    {
        var query = "SELECT * FROM \"Users\" WHERE \"Email\" = @Email AND \"Password\" = @Password";
        return await _dbConnection.QueryFirstOrDefaultAsync<User>(query, new { Email = email, Password = password });
    }

    public async Task<User> CreateAsync(User user)
    {
        var query = "INSERT INTO \"Users\" (\"Email\", \"Password\") VALUES (@Email, @Password) RETURNING *";
        return await _dbConnection.QuerySingleAsync<User>(query, user);
    }

    public async Task<User> FindByIdAsync(int id)
    {
        var query = "SELECT * FROM \"Users\" WHERE id = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<User>(query, new { Id = id });
    }
}
