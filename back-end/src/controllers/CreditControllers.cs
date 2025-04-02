using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

using back_end.src.models;

[ApiController]
[Route("credit")]
public class CreditController : ControllerBase
{
    private readonly CreditService _creditService;

    public CreditController(CreditService creditService)
    {
        _creditService = creditService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCredits()
    {
        try
        {
            var credits = await _creditService.GetCredits();
            return Ok(credits);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllCredits(bool allCredits)
    {
        try
        {
            var credits = await _creditService.GetAllCredits(allCredits);
            return Ok(credits);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{creditId}")]
    public async Task<IActionResult> GetCreditById(int creditId)
    {
        try
        {
            var credit = await _creditService.GetCredit(creditId);
            return Ok(credit);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> StoreCredit([FromBody] Credit creditDto)
    {
        try
        {
            var credit = await _creditService.StoreCredit(creditDto.Description, creditDto.Amount, creditDto.Creditor, creditDto.CreatedAt);
            return Ok(credit);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("payment/{creditId}")]
    public async Task<IActionResult> PayCredit(int creditId, [FromBody] Credit creditDto)
    {
        try
        {
            var credit = await _creditService.PayCredit(creditId, creditDto.Paid, creditDto.PayDate);
            return Ok(credit);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{creditId}")]
    public async Task<IActionResult> DeleteCredit(int creditId)
    {
        try
        {
            await _creditService.DeleteCredit(creditId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}