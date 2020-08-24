using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VPX.BusinessLogic.Core.Contracts.KnowledgeTests;
using VPX.BusinessLogic.Core.Contracts.TestTemplates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VPX.ApiModels;
using VPX.BusinessLogic.Constants;

namespace VPX.Presentation.WebClient.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TestTemplatesController : ControllerBase
    {
        private readonly IKnowledgeTestService knowledgeTestService;
        private readonly ITestTemplatesService testTemplatesService;

        public TestTemplatesController(IKnowledgeTestService knowledgeTestService,
            ITestTemplatesService testTemplatesService)
        {
            this.knowledgeTestService = knowledgeTestService;
            this.testTemplatesService = testTemplatesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CardTestTemplateModel>>> Get()
        {
            var cards = await testTemplatesService.GetCardTemplates();
            return Ok(cards);
        }

        [HttpPost]
        [Authorize(Roles = AppRoles.TeacherOrAdmin)]
        public async Task<ActionResult<TestTemplateModel>> Create(TestTemplateModel model)
        {
            var testTemplate = await testTemplatesService.Create(model);
            return Ok(testTemplate);
        }

        [HttpPost]
        [Route("execute")]
        public async Task<ActionResult> Execute(ExecuteTestModel model)
        {
            var test = await testTemplatesService.Execute(model);
            return Ok(test);
        }

        [HttpPost]
        [Route("submit")]
        public async Task<ActionResult> Submit(KnowledgeTestModel model)
        {
            var test = await testTemplatesService.Submit(model);
            return Ok(test);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<ActionResult> Delete(DeleteTestModel model)
        {
            model = await testTemplatesService.Delete(model);
            return Ok(model);
        }

        [HttpGet]
        [Route("results/{id}/for-user/{userId}")]
        public async Task<ActionResult> Results(Guid id, Guid userId)
        {
            var testResults = await knowledgeTestService.Results(id, userId);
            return Ok(testResults);
        }
    }
}
 