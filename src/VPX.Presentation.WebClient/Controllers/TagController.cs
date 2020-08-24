using System.Collections.Generic;
using System.Threading.Tasks;
using VPX.BusinessLogic.Core.Contracts.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VPX.ApiModels;

namespace VPX.Presentation.WebClient.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TagController : ControllerBase
    {
        private readonly ITagService tagService;

        public TagController(ITagService tagService)
        {
            this.tagService = tagService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TagModel>>> GetAll()
        {
            var tags = await tagService.GetAsync();
            return Ok(tags);
        }
    }
}
