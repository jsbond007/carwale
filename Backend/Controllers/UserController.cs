using Microsoft.AspNetCore.Mvc;
using Carwale.Objects;
using Carwale.API;
using Carwale.Domain.Repositories.MakeRepository;
using Carwale.Services;
using Carwale.Services.UserService;

namespace Carwale.Controllers
{

    [Route("[controller]")]
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [Uniform]
        [HttpPost("login")]
		[ProducesResponseType(typeof(ApiResponse<AuthenticateResponse>), 200)]
		[ProducesResponseType(typeof(BaseResponse), 400)]
		public async Task<IActionResult> Authenticate(LoginRequest loginRequest)
        {
            var response=await this._userService.Authenticate(loginRequest.UserName, loginRequest.Password);
            return Ok(response);
        }
    }
}