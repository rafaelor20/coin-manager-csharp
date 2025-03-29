using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

using back_end.src.models;

[ApiController]
[Route("transaction")]

public class transactionController : ControllerBase

{
    private readonly TransactionService _transactionService;

    public transactionController(TransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet]
    public async Task<IActionResult> getHistoric()
    {
        try
        {
            var historic = await _transactionService.GetHistoric();
            return Ok(historic);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> StoreTransaction([FromBody] Transaction transactionDto)
    {
        try
        {
            var transaction = await _transactionService.StoreTransaction(transactionDto.Description, transactionDto.Amount, transactionDto.Entity, transactionDto.CreatedAt);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{transactionId}")]
    public async Task<IActionResult> DeleteTransaction(int transactionId)
    {
        try
        {
            await _transactionService.DeleteTransaction(transactionId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

