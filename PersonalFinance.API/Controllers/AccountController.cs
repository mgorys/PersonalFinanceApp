using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Abstractions.IServices;
using PersonalFinance.Models.Dtos;

namespace PersonalFinance.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterDto dto)
        {
            await _accountService.RegisterUserAsync(dto);
            return StatusCode(201);
        }
        [HttpPost("login")]
        public async Task<ActionResult<LoggedUserInfo>> Login([FromBody] LoginDto loginDto)
        {
            LoggedUserInfo user = await _accountService.LoginUserAsync(loginDto);
            return Ok(user);

        }
    }
}

