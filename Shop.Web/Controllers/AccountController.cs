using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Web.Dtos;
using Shop.Web.Services;

namespace Shop.Web.Controllers
{
    [ApiController, Route("account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost, Route("[action]")]
        public IActionResult Register([FromBody]RegisterDto dto)
        {
            _accountService.Register(dto);

            return Ok();
        }

        [HttpPost, Route("[action]")]
        public IActionResult Login([FromBody]LoginDto dto)
        {
            var token = _accountService.Login(dto);

            return Ok(new { Token = token });
        }
    }
}
