using System;
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
        private readonly IUserRolesService userRolesService;

        public UsersController(IUsersService usersService,
            ICurrentUser currentUser,
            IUserRolesService userRolesService)
        {
            this.usersService = usersService;
            this.currentUser = currentUser;
            this.userRolesService = userRolesService;
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

        [HttpGet]
        [Route("{id}/profile")]
        public async Task<ActionResult> Profile(Guid id)
        {
            var profile = await usersService.GetProfile(id);
            return Ok(profile);
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> Update(UserUpdatesModel model)
        {
            var user = await usersService.Update(model);
            return Ok(user);
        }

        [HttpPost]
        [Route("lock")]
        public async Task<ActionResult> Lock(IdModel model)
        {
            var profile = await usersService.Lock(model.Id);
            return Ok(profile);
        }

        [HttpPost]
        [Route("unlock")]
        public async Task<ActionResult> Unlock(IdModel model)
        {
            var profile = await usersService.Unlock(model.Id);
            return Ok(profile);
        }

        [HttpPost]
        [Route("addteacher")]
        public async Task<ActionResult> AddTeacher(IdModel model)
        {
            var profile = await userRolesService.AddRole(model.Id, Domain.Enums.Role.Teacher);
            return Ok(profile);
        }

        [HttpPost]
        [Route("removeteacher")]
        public async Task<ActionResult> RemoveTeacher(IdModel model)
        {
            var profile = await userRolesService.RemoveRole(model.Id, Domain.Enums.Role.Teacher);
            return Ok(profile);
        }

        [HttpPost]
        [Route("addstudent")]
        public async Task<ActionResult> AddStudent(IdModel model)
        {
            var profile = await userRolesService.AddRole(model.Id, Domain.Enums.Role.Student);
            return Ok(profile);
        }

        [HttpPost]
        [Route("removestudent")]
        public async Task<ActionResult> RemoveStudent(IdModel model)
        {
            var profile = await userRolesService.RemoveRole(model.Id, Domain.Enums.Role.Student);
            return Ok(profile);
        }

        [HttpGet]
        [Route("users-without-group")]
        public async Task<ActionResult> GetUsersWithoutGroup()
        {
            var users = await usersService.GetUsersWithoutGroups();
            return Ok(users);
        }
    }
}