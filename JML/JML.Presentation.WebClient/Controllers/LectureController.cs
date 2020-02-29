using JML.ApiModels;
using JML.BusinessLogic.Core.Contracts.Lectures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JML.Presentation.WebClient.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LectureController : ControllerBase
    {
        private readonly ILectureService lectureService;

        public LectureController(ILectureService lectureService)
        {
            this.lectureService = lectureService;
        }

        [HttpGet]
        public async Task<ActionResult<List<LectureModel>>> GetAll()
        {
            var lectures = await lectureService.GetAsync();
            return Ok(lectures);
        }

        [HttpGet]
        [Route("{url}")]
        public async Task<IActionResult> GetByUrl(string url)
        {
            var lecture = await lectureService.GetAsync(url);
            return Ok(lecture);
        }

        [HttpPost]
        public async Task<IActionResult> Post(LectureModel model)
        {
            var lecture = await lectureService.CreateAsync(model);
            return Ok(lecture);
        }
    }
}
