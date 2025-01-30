using Microsoft.AspNetCore.Mvc;
using WebApplication2.Application.Services;
using WebApplication2.Presentation.Models;

namespace WebApplication2.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AtmController : ControllerBase
{
    private readonly AtmService _atmService;

    public AtmController(AtmService atmService)
    {
        _atmService = atmService;
    }
    
    [HttpPost("register")]
    public IActionResult Register([FromBody] UserRegisterRequest request)
    {
        try
        {
            _atmService.Register(request.AccountNumber, request.Pin, request.FullName);
            return Ok("Регистрация успешна!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginRequest request)
    {
        try
        {
            var account = _atmService.Login(request.AccountNumber, request.Pin);
            return Ok(account);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("deposit")]
    public IActionResult Deposit([FromBody] DepositRequest request)
    {
        try
        {
            var account = _atmService.Login(request.AccountNumber, request.Pin);
            _atmService.Deposit(account, request.Amount);
            return Ok("Счет успешно пополнен.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("withdraw")]
    public IActionResult Withdraw([FromBody] WithdrawRequest request)
    {
        try
        {
            var account = _atmService.Login(request.AccountNumber, request.Pin);
            _atmService.Withdraw(account, request.Amount);
            return Ok("Деньги успешно сняты.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("history")]
    public IActionResult GetHistory([FromQuery] string accountNumber, [FromQuery] string pin)
    {
        try
        {
            var account = _atmService.Login(accountNumber, pin);
            var transactions = _atmService.GetTransactionHistory(account);
            return Ok(transactions);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return Ok("Вы вышли из системы.");
    }
    
    [HttpGet("balance")]
    public IActionResult GetBalance([FromQuery] string accountNumber, [FromQuery] string pin)
    {
        try
        {
            var account = _atmService.Login(accountNumber, pin);
            var balance = _atmService.GetBalance(account);
            return Ok(new { balance });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}