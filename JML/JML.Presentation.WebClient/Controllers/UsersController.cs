using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JML.ApiModels;
using JML.BusinessLogic.Core.Contracts.Accounts;
using JML.BusinessLogic.Core.Contracts.Users;
using JML.BusinessLogic.Mappings.Users;
using JML.DataAccess.Core.Contracts;
using JML.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JML.Presentation.WebClient.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;
        private readonly IAppEntityRepository<User> usersRepository;
        private readonly ICurrentUser currentUser;

        public UsersController(IUsersService usersService,
            IAppEntityRepository<User> usersRepository,
            ICurrentUser currentUser)
        {
            this.usersService = usersService;
            this.usersRepository = usersRepository;
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

        [HttpGet]
        [Route("{id}/profile")]
        public async Task<ActionResult> Profile(Guid id)
        {
            var user = await usersRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == id);
            var profile = new UserProfileModel
            {
                ActiveAt = user.ActiveAt,
                CreatedAt = user.CreatedAt,
                FullName = user.FirstName + " " + user.LastName,
                GroupName = user.Group?.Name,
                IsLocked = user.IsLocked,
                Roles = user.UserRoles.Select(x => UserRoleMap.Map(x.Role)).ToList(),
                HasStudentRole = user.UserRoles.Any(x => x.Role == Domain.Enums.Role.Student),
                HasTeacherRole = user.UserRoles.Any(x => x.Role == Domain.Enums.Role.Teacher),
                Tests = user.Tests.Select(x => x.TestTemplate.Name).Distinct().ToList()
            };

            return Ok(profile);
        }
    }
}