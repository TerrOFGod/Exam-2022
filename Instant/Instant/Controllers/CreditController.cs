using Instant.Entities;
using Instant.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoansIssuance.Controllers;

[ApiController]
[Route("credit")]
public class CreditController : ControllerBase
{
    private readonly CreditService _creditService;

    public CreditController(CreditService creditService)
    {
        _creditService = creditService;
    }
    
    [HttpPost("take")]
    public IActionResult TakeCredit([FromBody] Request request)
    {
        var result = _creditService.GetCreditResult(request);
        return Ok(result);
    }
}