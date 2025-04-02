using System;
using System.Threading.Tasks;

using back_end.src.models;

public class DebtService(DebtRepository debtRepository, UserRepository userRepository)
{
    private readonly DebtRepository _debtRepository;
    private readonly UserRepository _userRepository;

    async Task CheckUserByDebtId(int debtId, int userId)
    {
        var debt = await _debtRepository.FindByIdAsync(debtId);
        if (debt == null)
        {
            throw new Exception("UnauthorizedError");
        }

        if (debt.UserId != userId)
        {
            throw new Exception("UnauthorizedError");
        }
    }

    async Task checkUserById(int id)
    {
        var user = await _userRepository.FindByIdAsync(id);
        if (user == null)
        {
            throw new Exception("UnauthorizedError");
        }
    }

    Task checkAmount(decimal amount)
    {
        if (amount.GetType() != typeof(decimal))
        {
            throw new Exception("InvalidAmountError");
        }
        return Task.CompletedTask;
    }

    async Task getDebtsByUserId(int id)
    {
        var debts = await _debtRepository.GetDebtsByUserId(id);
        if (debts == null)
        {
            throw new Exception("InvalidDebtIdError");
        }
    }

    async Task getAllDebtsByUserId(int id)
    {
        var debts = await _debtRepository.GetAllDebtsByUserId(id);
        if (debts == null)
        {
            throw new Exception("InvalidDebtIdError");
        }
    }

    async Task getDebtById(int id)
    {
        var debt = await _debtRepository.FindByIdAsync(id);
        if (debt == null)
        {
            throw new Exception("InvalidDebtIdError");
        }
    }

    async Task storeDebt(Debt debt)
    {
        var storedDebt = await _debtRepository.CreateAsync(debt);
        if (storedDebt == null)
        {
            throw new Exception("ErrorCreatingDebt");
        }
    }

    async Task deleteDebt(int id)
    {
        var deletedDebt = await _debtRepository.DeleteDebt(id);
        if (deletedDebt == null)
        {
            throw new Exception("InvalidDebtIdError");
        }
    }

    async Task partialPayment(Debt debt, decimal amount)
    {
        var newAmount = debt.Amount - amount;
        _debtRepository.UpdateDebtAmount(debt.Id, newAmount);
    }

    async Task fullPayment(Debt debt)
    {
        _debtRepository.payDebt(debt.Id);
    }

    async Task payment(int userId, int debtId, decimal amount)
    {
        var debt = await _debtRepository.FindByIdAsync(debtId);
        if (debt.Amount >= amount)
        {
            await partialPayment(debt, amount);
        }
        else
        {
            await fullPayment(debt);
        }
    }

}