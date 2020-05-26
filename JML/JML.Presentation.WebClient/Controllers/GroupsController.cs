using JML.ApiModels;
using JML.BusinessLogic.Core.Contracts.Groups;
using JML.DataAccess.Core.Contracts;
using JML.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JML.Presentation.WebClient.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService groupService;
        private readonly IAppEntityRepository<StudyGroup> studyGroupsRepository;
        private readonly IAppEntityRepository<User> usersRepository;
        private readonly IDataContext dataContext;

        public GroupsController(IGroupService groupService,
            IAppEntityRepository<StudyGroup> studyGroupsRepository,
            IAppEntityRepository<User> usersRepository,
            IDataContext dataContext)
        {
            this.groupService = groupService;
            this.studyGroupsRepository = studyGroupsRepository;
            this.usersRepository = usersRepository;
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<GroupModel>>> GetAll()
        {
            var groups = await groupService.GetAllSimple();
            return Ok(groups);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<GroupModel>>> Get(Guid id)
        {
            var group = await groupService.GetSimple(id);
            return Ok(group);
        }

        [HttpPost]
        public async Task<ActionResult<GroupModel>> Create(GroupModel model)
        {
            return Ok(await groupService.Create(model.Name));
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> Update(UpdateGroupModel model)
        {
            var group = await studyGroupsRepository.GetQuery().FirstAsync(x => x.Id == model.Id);

            if (group != null)
            {
                group.Name = model.Name;
                
                foreach (var user in await usersRepository.GetQuery().ToListAsync())
                {
                    user.GroupId = null;
                    if (model.Users != null && model.Users.Contains(user.Id))
                    {
                        user.GroupId = group.Id;
                    }
                }

                await dataContext.SaveChangesAsync();
            }

            return Ok();
        }
    }
}