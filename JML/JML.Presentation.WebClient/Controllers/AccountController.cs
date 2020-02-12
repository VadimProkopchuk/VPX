using System.Threading.Tasks;
using JML.ApiModels;
using JML.BusinessLogic.Core.Contracts.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace JML.Presentation.WebClient.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var jwt = await authenticationService.AuthAsync(loginModel.Email, loginModel.Password);
            var tokenPair = new TokenPair
            {
                Token = jwt.Token,
                ExpiredAt = jwt.ExpiredAt
            };

            return Ok(tokenPair);
        }
    }
}
