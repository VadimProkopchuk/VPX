using System.Linq;
using System.Threading.Tasks;
using JML.ApiModels;
using JML.BusinessLogic.Core.Contracts.Accounts;
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
            var userModel = new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                GroupName = user.Group?.Name,
                Roles = user.UserRoles?.Select(x => x.Role.ToString()).ToArray() ?? new string[0], 
            };

            return Ok(userModel);
        }
    }
}
