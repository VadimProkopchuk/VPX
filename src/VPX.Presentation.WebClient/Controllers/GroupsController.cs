using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VPX.BusinessLogic.Core.Contracts.Groups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VPX.ApiModels;

namespace VPX.Presentation.WebClient.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService groupService;

        public GroupsController(IGroupService groupService)
        {
            this.groupService = groupService;
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
            var group = await groupService.Create(model.Name);
            return Ok(group);
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> Update(UpdateGroupModel model)
        {
            await groupService.Update(model);
            return Ok();
        }
    }
}