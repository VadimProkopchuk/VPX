using JML.ApiModels;
using JML.BusinessLogic.Core.Contracts.Groups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JML.Presentation.WebClient.Controllers
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

        [HttpPost]
        public async Task<ActionResult<GroupModel>> Create(GroupModel model)
        {
            return Ok(await groupService.Create(model.Name));
        }
    }
}