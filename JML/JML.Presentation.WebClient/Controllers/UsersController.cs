using System.Collections.Generic;
using System.Threading.Tasks;
using JML.ApiModels;
using JML.BusinessLogic.Core.Contracts.Accounts;
using JML.BusinessLogic.Core.Contracts.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JML.Presentation.WebClient.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;
        private readonly ICurrentUser currentUser;

        public UsersController(IUsersService usersService,
            ICurrentUser currentUser)
        {
            this.usersService = usersService;
            this.currentUser = currentUser;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("HasUserByEmail/{email}")]
        public async Task<ActionResult<bool>> HasUserByEmail(string email)
        {
            var hasAny = await usersService.HasUserByEmailAsync(email);
            return Ok(hasAny);
        }

        [HttpGet]
        [Route("current")]
        public async Task<ActionResult<UserModel>> GetCurrentUser()
        {
            var user = await currentUser.GetCurrentUserAsync();
            return Ok(user);
        }

        [HttpGet]
        [Route("teachers")]
        public async Task<ActionResult<List<UserModel>>> GetTeachers()
        {
            var users = await usersService.GetTeachers();
            return Ok(users);
        }
    }
}