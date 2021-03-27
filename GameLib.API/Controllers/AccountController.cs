using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameLib.API.Auth;
using GameLib.Model.DTOs;
using GameLib.Model.Entity;
using GameLib.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GameLib.API.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : UserScopedController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            IUserService userService,
            IConfiguration configuration,
            ILogger<AccountController> logger
        )
        {
            _userService = userService;
            _configuration = configuration;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]LoginDTO model)
        {
            var user = await _userService.FindToLogin(model.Username, Utils.ToHashMd5(model.Password));
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });
            
            var token = TokenService.GenerateToken(user, _configuration["App:Secret"]);

            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("sign_in")]
        public async Task<ActionResult<dynamic>> SignIn([FromBody]UserDTO model)
        {
            var password = model.Password;
            var newUser = new User
            {
                Nickname = model.Nickname,
                Username = model.Username,
                Password = Utils.ToHashMd5(model.Password)
            };
            model.Password = Utils.ToHashMd5(model.Password);
            var user = await _userService.CreateNewUser(newUser, password);
            user.Password = null;

            return new { user };
        }

        [HttpGet]
        [Route("me")]
        public ActionResult<object> CurrentUserInfo()
        {
            return new {
                Username = User.Identity.Name,
                Id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
            };
        }
    }
}
