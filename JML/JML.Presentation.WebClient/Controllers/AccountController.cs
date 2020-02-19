using System.Linq;
using System.Threading.Tasks;
using JML.ApiModels;
using JML.BusinessLogic.Core.Contracts.Accounts;
using JML.Presentation.WebClient.Infrastructure.Presenters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JML.Presentation.WebClient.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;
        private readonly ICurrentUser currentUser;

        public AccountController(IAuthenticationService authenticationService,
            ICurrentUser currentUser)
        {
            this.authenticationService = authenticationService;
            this.currentUser = currentUser;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var jwt = await authenticationService.AuthAsync(model.Email, model.Password);
            var tokenPair = new TokenPair
            {
                Token = jwt.Token,
                ExpiredAt = jwt.ExpiredAt
            };

            return Ok(tokenPair);
        }

        [HttpGet]
        [Route("current-user")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await currentUser.GetCurrentUserAync();
            var roles = user.UserRoles?.Select(x => x.Role.Present()).ToArray() ?? new string[0];
            var userModel = new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                GroupName = user.Group?.Name,
                Roles = roles,
            };

            return Ok(userModel);
        }
    }
}
