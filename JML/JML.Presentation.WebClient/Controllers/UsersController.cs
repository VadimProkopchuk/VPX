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
        private readonly IAppEntityRepository<UserRole> userRolesRepository;
        private readonly IDataContext dataContext;
        private readonly ICurrentUser currentUser;

        public UsersController(IUsersService usersService,
            IAppEntityRepository<User> usersRepository,
            IAppEntityRepository<UserRole> userRolesRepository,
            IDataContext dataContext,
            ICurrentUser currentUser)
        {
            this.usersService = usersService;
            this.usersRepository = usersRepository;
            this.userRolesRepository = userRolesRepository;
            this.dataContext = dataContext;
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

            return Ok(GetProfile(user));
        }

        private UserProfileModel GetProfile(User user)
        {
            return new UserProfileModel
            {
                ActiveAt = user.ActiveAt,
                CreatedAt = user.CreatedAt,
                FullName = user.FirstName + " " + user.LastName,
                GroupName = user.Group?.Name,
                IsLocked = user.IsLocked,
                Roles = user.UserRoles.Select(x => UserRoleMap.Map(x.Role)).ToList(),
                HasStudentRole = user.UserRoles.Any(x => x.Role == Domain.Enums.Role.Student),
                HasTeacherRole = user.UserRoles.Any(x => x.Role == Domain.Enums.Role.Teacher),
                Tests = user.Tests
                    .Select(x => new TestNameModel 
                    { 
                        Name = x.TestTemplate.Name, 
                        Id = x.TestTemplateId 
                    })
                    .GroupBy(x => x.Id)
                    .Select(x => x.First())
                    .ToList(),
                Image = user.AvatarBase64
            };
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> Update(UserUpdatesModel model)
        {
            var user = await currentUser.GetUser();

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            if (!String.IsNullOrEmpty(model.NewPassword))
            {
                if (user.Password == model.Password)
                {
                    user.Password = model.NewPassword;
                }
            }

            if (model.Image != null)
            {
                user.AvatarBase64 = model.Image;
            }

            await dataContext.SaveChangesAsync();

            return Ok(await currentUser.GetCurrentUserAsync());
        }

        [HttpPost]
        [Route("lock")]
        public async Task<ActionResult> Lock(IdModel model)
        {
            var user = await usersRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == model.Id);
            
            if (user != null)
            {
                user.IsLocked = true;
                await dataContext.SaveChangesAsync();
                return Ok(GetProfile(user));
            }

            return Ok();
        }

        [HttpPost]
        [Route("unlock")]
        public async Task<ActionResult> Unlock(IdModel model)
        {
            var user = await usersRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == model.Id);

            if (user != null)
            {
                user.IsLocked = false;
                user.CountOfInvalidAttempts = 0;
                await dataContext.SaveChangesAsync();
                return Ok(GetProfile(user));
            }

            return Ok();
        }

        [HttpPost]
        [Route("addteacher")]
        public async Task<ActionResult> AddTeacher(IdModel model)
        {
            var user = await usersRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == model.Id);

            if (user != null && !user.UserRoles.Any(x => x.Role == Domain.Enums.Role.Teacher))
            {
                user.UserRoles.Add(new UserRole { Role = Domain.Enums.Role.Teacher });
                await dataContext.SaveChangesAsync();
                return Ok(GetProfile(user));
            }

            return Ok();
        }

        [HttpPost]
        [Route("removeteacher")]
        public async Task<ActionResult> RemoveTeacher(IdModel model)
        {
            var user = await usersRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == model.Id);

            if (user != null)
            {
                var roles = user.UserRoles.Where(x => x.Role == Domain.Enums.Role.Teacher).ToList();

                foreach (var role in roles)
                {
                    userRolesRepository.Remove(role);
                }

                await dataContext.SaveChangesAsync();
                return Ok(GetProfile(user));
            }

            return Ok();
        }

        [HttpPost]
        [Route("addstudent")]
        public async Task<ActionResult> AddStudent(IdModel model)
        {
            var user = await usersRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == model.Id);

            if (user != null && !user.UserRoles.Any(x => x.Role == Domain.Enums.Role.Student))
            {
                user.UserRoles.Add(new UserRole { Role = Domain.Enums.Role.Student });
                await dataContext.SaveChangesAsync();
                return Ok(GetProfile(user));
            }

            return Ok();
        }

        [HttpPost]
        [Route("removestudent")]
        public async Task<ActionResult> RemoveStudent(IdModel model)
        {
            var user = await usersRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == model.Id);

            if (user != null)
            {
                var roles = user.UserRoles.Where(x => x.Role == Domain.Enums.Role.Student).ToList();

                foreach (var role in roles)
                {
                    userRolesRepository.Remove(role);
                }

                await dataContext.SaveChangesAsync();
                return Ok(GetProfile(user));
            }

            return Ok();
        }

        [HttpGet]
        [Route("users-without-group")]
        public async Task<ActionResult> GetUsersWithoutGroup()
        {
            var users = await usersRepository.GetQuery().Where(x => x.GroupId == null).ToListAsync();

            return Ok(users.Select(x => new UserWithoutGroupModel
            {
                Value = x.Id,
                Display = $"{x.FirstName} {x.LastName}"
            }).ToList());
        }
    }
}