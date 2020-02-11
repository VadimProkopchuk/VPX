using System.Threading.Tasks;
using JML.BusinessLogic.Core.Contracts.Accounts;
using JML.Models;
using Microsoft.AspNetCore.Mvc;

namespace JML.Presentation.WebClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpGet]
        public async Task<JwtModel> Get()
        {
            return await authenticationService.AuthAsync("asda@asd.as", "123");
        }
    }
}
