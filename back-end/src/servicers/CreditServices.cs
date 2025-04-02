using System;
using System.Threading.Tasks;

using back_end.src.models;

public class CreditService(CreditRepository creditRepository, UserRepository userRepository)
{
    private readonly CreditRepository _creditRepository;
    private readonly UserRepository _userRepository;

    async Task CheckUserByCreditId(int creditId, int userId)
    {
        var credit = await _creditRepository.FindByIdAsync(creditId);
        if (credit == null)
        {
            throw new Exception("UnauthorizedError");
        }

        if (credit.userId != userId)
        {
            throw new Exception("UnauthorizedError");
        }
    }

    async Task CheckUserById(int id)
    {
        var user = await _userRepository.FindByIdAsync(id);
        if (user == null)
        {
            throw new Exception("UnauthorizedError");
        }
    }

    async Task getCreditById(int id)
    {
        var credit = await _creditRepository.FindByIdAsync(id);
        if (credit == null)
        {
            throw new Exception("InvalidCreditIdError");
        }
    }

    async Task getCreditsByUserId(int id)
    {
        var credits = await _creditRepository.GetCreditsByUserId(id);
        if (credits == null)
        {
            throw new Exception("InvalidCreditIdError");
        }
    }

    async Task getAllCreditsbyUserId(int id)
    {
        var credits = await _creditRepository.GetAllCreditsByUserId(id);
        if (credits == null)
        {
            throw new Exception("InvalidCreditIdError");
        }
    }

    async Task storeCredit(Credit credit)
    {
        var storedCredit = await _creditRepository.StoreCredit(credit);
        if (storedCredit == null)
        {
            throw new Exception("InvalidCreditIdError");
        }
    }

    async Task deleteCredit(int id)
    {
        var deletedCredit = await _creditRepository.DeleteCredit(id);
        if (deletedCredit == null)
        {
            throw new Exception("InvalidCreditIdError");
        }
    }

    async Task partialPayment(Credit credit, decimal amount)
    {
        var newAmount = credit.amount - amount;
        _creditRepository.UpdateCredit(credit, newAmount);

    }
    async Task fullPayment(Credit credit)
    {
        var fullPayment = await _creditRepository.FullPayment(credit);
    }

    async Task payment(int userId, int creditId, decimal amount)
    {
        var credit = await _creditRepository.FindByIdAsync(creditId);
        if (credit.amount >= amount)
        {
            fullPayment(credit);
        }
        else
        {
            partialPayment(credit, amount);
        }
    }
}