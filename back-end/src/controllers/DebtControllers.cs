using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

using back_end.src.models;

[ApiController]
[Route("debt")]

public class debtController : ControllerBase
{
    private readonly DebtService _debtService;

    public debtController(DebtService debtService)
    {
        _debtService = debtService;
    }

    [HttpGet]
    public async Task<IActionResult> getDebts()
    {
        try
        {
            var historic = await _debtService.getDebts();
            return Ok(historic);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("all")]
    public async Task<IActionResult> getAllDebts()
    {
        try
        {
            var historic = await _debtService.getAllDebts();
            return Ok(historic);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{debtId}")]
    public async Task<IActionResult> getDebtById(int debtId)
    {
        try
        {
            var debt = await _debtService.getDebtById(debtId);
            return Ok(debt);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> StoreDebt([FromBody] Debt debtDto)
    {
        try
        {
            var debt = await _debtService.StoreDebt(debtDto.Description, debtDto.Creditor, debtDto.Amount, debtDto.CreatedAt);
            return Ok(debt);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{debtId}")]
    public async Task<IActionResult> DeleteDebt(int debtId)
    {
        try
        {
            await _debtService.DeleteDebt(debtId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("payment/{debtId}")]
    public async Task<IActionResult> PayDebt(int debtId, [FromBody] Debt debtDto)
    {
        try
        {
            var debt = await _debtService.PayDebt(debtId, debtDto.Paid, debtDto.PayDate);
            return Ok(debt);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}