using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using diaryApp_backend;
using diaryApp_backend.Services.Interfaces;

namespace diaryApp_backend.Controllers
{
    [Route("api/auth")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("login/{email}/{password}")]
        public async Task<ActionResult<User>> Login(string email, string password)
        {
            return await _authService.Login(email, password);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody]User user, string password) {
            return await _authService.Register(user, password);
        }

        
    }
}

