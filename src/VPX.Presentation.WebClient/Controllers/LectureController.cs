using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VPX.BusinessLogic.Core.Contracts.Lectures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VPX.ApiModels;
using VPX.BusinessLogic.Constants;

namespace VPX.Presentation.WebClient.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LectureController : ControllerBase
    {
        private readonly ILectureService lectureService;

        public LectureController(ILectureService lectureService)
        {
            this.lectureService = lectureService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SectionGroupModel>>> GetAll()
        {
            var groups = await lectureService.GetAll();
            return Ok(groups);
        }

        [HttpGet]
        [Route("{url}")]
        public async Task<ActionResult<LectureModel>> GetByUrl(string url)
        {
            var lecture = await lectureService.GetAsync(url);
            return Ok(lecture);
        }

        [HttpPost]
        [Authorize(Roles = AppRoles.TeacherOrAdmin)]
        public async Task<ActionResult<LectureModel>> Create(LectureModel model)
        {
            var lecture = await lectureService.CreateAsync(model);
            return Ok(lecture);
        }

        [HttpPut]
        [Authorize(Roles = AppRoles.TeacherOrAdmin)]
        public async Task<ActionResult<LectureModel>> Update(LectureModel model)
        {
            var lecture = await lectureService.UpdateAsync(model);
            return Ok(lecture);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = AppRoles.TeacherOrAdmin)]
        public async Task<ActionResult<LectureModel>> Delete(Guid id)
        {
            var lecture = await lectureService.RemoveAsync(id);
            return Ok(lecture);
        }
    }
}
