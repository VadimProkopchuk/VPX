using System.Threading.Tasks;
using JML.ApiModels;
using JML.BusinessLogic.Core.Contracts.Users;
using Microsoft.AspNetCore.Mvc;

namespace JML.Presentation.WebClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        [Route("HasUserByEmail/{email}")]
        public async Task<ActionResult<bool>> HasUserByEmail(string email)
        {
            var user = await usersService.GetByEmailAsync(email);
            return Ok(user != null);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterUserModel user)
        {
            await Task.Delay(2000);
            return Ok();
        }
    }
}