using System;
using System.Threading.Tasks;

using back_end.src.models;

public class UserService(UserRepository userRepository)
{
    private readonly UserRepository _userRepository = userRepository;

    public async Task<User> CreateUserAsync(string Email, string Password)
    {
        await ValidateUniqueEmailOrFail(Email);
        await ValidateCreateUserParamsOrFail(Email, Password);

        return await _userRepository.CreateAsync(new User { Email = Email, Password = Password });
    }

    public async Task<User> AuthenticateAsync(string Email, string Password)
    {
        var user = await _userRepository.FindByEmailAndPasswordAsync(Email, Password);
        if (user == null)
        {
            throw new Exception("InvalidCredentialsError");
        }
        return user;
    }

    private async Task ValidateUniqueEmailOrFail(string Email)
    {
        var userWithSameEmail = await _userRepository.FindByEmailAsync(Email);
        if (userWithSameEmail != null)
        {
            throw new Exception("DuplicatedEmailError");
        }
    }

    private Task ValidateCreateUserParamsOrFail(string Email, string Password)
    {
        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            throw new Exception("ValidationError");
        }
        return Task.CompletedTask;
    }
}

