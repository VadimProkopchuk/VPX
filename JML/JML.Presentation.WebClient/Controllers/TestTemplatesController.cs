using JML.ApiModels;
using JML.BusinessLogic.Constants;
using JML.BusinessLogic.Core.Contracts.Accounts;
using JML.DataAccess.Core.Contracts;
using JML.Domain;
using JML.Domain.Templates;
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
    public class TestTemplatesController : ControllerBase
    {
        private readonly IAppEntityRepository<TestTemplate> testTemplateRepository;
        private readonly IAppEntityRepository<KnowledgeTest> knowledgeTestRepository;
        private readonly ICurrentUser currentUser;
        private readonly IDataContext dataContext;

        public TestTemplatesController(IAppEntityRepository<TestTemplate> testTemplateRepository,
            IAppEntityRepository<KnowledgeTest> knowledgeTestRepository,
            ICurrentUser currentUser,
            IDataContext dataContext)
        {
            this.testTemplateRepository = testTemplateRepository;
            this.knowledgeTestRepository = knowledgeTestRepository;
            this.currentUser = currentUser;
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<CardTestTemplateModel>>> Get()
        {
            var templates = await testTemplateRepository
                .GetQuery()
                .Where(x => x.IsActive)
                .ToListAsync();
            var user = await currentUser.GetCurrentUserAsync();
            var userTests = (await knowledgeTestRepository
                .GetQuery()
                .Where(x => x.UserId == user.Id)
                .ToListAsync())
                .GroupBy(x => x.TestTemplateId);
            
            var templateCards = templates
                .Select(x => new CardTestTemplateModel
                {
                   Id = x.Id,
                   Name = x.Name,
                   Description = x.Description,
                   CreatedAt = x.CreatedAt,
                   CountOfQuestions = x.CountOfQuestions,
                })
                .ToList();

            foreach (var userTestGroup in userTests)
            {
                var lastTest = userTestGroup.OrderByDescending(f => f.ModifiedAt).First();
                var card = templateCards.FirstOrDefault(x => x.Id == userTestGroup.Key);

                if (card != null)
                {
                    card.LastResult = lastTest.Questions.Count(x => x.IsProvidedCorrectAnswer);
                    card.Attempts = userTestGroup.Count();
                }
            }

            return Ok(templateCards);
        }

        [HttpPost]
        [Authorize(Roles = AppRoles.TeacherOrAdmin)]
        public async Task<ActionResult<TestTemplateModel>> Create(TestTemplateModel model)
        {
            var testTemplate = new TestTemplate
            {
                ExecuteTime = new TimeSpan(0, 10, 0),
                Name = model.Name,
                CountOfQuestions = model.CountOfQuestions,
                Description = model.Description,
                CreatedAt = model.CreatedAt,
                IsActive = true,
                Questions = model.Questions
                    .Select(x => new QuestionTemplate
                    {
                        Name = x.Name,
                        ControlType = x.ControlType,
                        Answers = x.Answers.Select(f => new AnswerTemplate
                        {
                            Answer = f.Answer,
                            IsCorrect = f.IsCorrect
                        }).ToList()
                    }).ToList()
            };
            testTemplateRepository.Add(testTemplate);

            await dataContext.SaveChangesAsync();

            return Ok(MapTestTemplate(testTemplate));
        }

        [HttpPost]
        [Route("execute")]
        public async Task<ActionResult> Execute(ExecuteTestModel model)
        {
            var template = await testTemplateRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == model.Id);

            if (!template.IsActive)
            {
                throw new ApplicationException();
            }

            var user = await currentUser.GetCurrentUserAsync();
            var knowledgeTest = new KnowledgeTest
            {
                UserId = user.Id,
                TestTemplateId = template.Id,
                ExpiredAt = DateTime.UtcNow.AddMinutes(10),
                Questions = template.Questions.Select(x => new KnowledgeTestQuestion
                {
                    QuestionTemplateId = x.Id,
                }).ToList(),
            };

            knowledgeTestRepository.Add(knowledgeTest);
            await dataContext.SaveChangesAsync();

            var test = new KnowledgeTestModel
            {
                Id = knowledgeTest.Id,
                Name = knowledgeTest.TestTemplate.Name,
                Questions = knowledgeTest.Questions.Select(x => new KnowledgeQuestionModel
                {
                    Id = x.Id,
                    Name = x.QuestionTemplate.Name,
                    ControlType = x.QuestionTemplate.ControlType,
                    Answers = x.QuestionTemplate.Answers.Select(f => new KnowledgeAnswerModel
                    {
                        Id = f.Id,
                        Answer = x.QuestionTemplate.ControlType == Domain.Enums.ControlType.Text ? null : f.Answer
                    }).ToList()
                }).ToList()
            };

            return Ok(test);
        }

        [HttpPost]
        [Route("submit")]
        public async Task<ActionResult> Submit(KnowledgeTestModel model)
        {
            var test = await knowledgeTestRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == model.Id);

            test.ModifiedAt = DateTime.Now;
            
            foreach (var question in model.Questions)
            {
                var knowledgeQuestion = test.Questions.FirstOrDefault(x => x.Id == question.Id);

                knowledgeQuestion.IsProvidedCorrectAnswer = IsProvidedCorrectAnswer(knowledgeQuestion, question);
            }

            await dataContext.SaveChangesAsync();

            return Ok(GetTestResult(test));
        }

        [HttpPost]
        [Route("delete")]
        public async Task<ActionResult> Delete(DeleteTestModel model)
        {
            var template = await testTemplateRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == model.Id);

            template.IsActive = false;
            await dataContext.SaveChangesAsync();

            return Ok(model);
        }


        private bool IsProvidedCorrectAnswer(KnowledgeTestQuestion question, KnowledgeQuestionModel model)
        {
            var correctAnswers = question.QuestionTemplate.Answers.Where(x => x.IsCorrect);
            
            switch (question.QuestionTemplate.ControlType)
            {
                case Domain.Enums.ControlType.Text:
                    var correctTextAnswer = correctAnswers.FirstOrDefault();
                    return model.Answers[0].Answer == correctTextAnswer.Answer;

                case Domain.Enums.ControlType.Single:
                    var correctSingleAnswer = correctAnswers.FirstOrDefault();
                    return model.AnswerId == correctSingleAnswer.Id;

                case Domain.Enums.ControlType.Multiple:
                    var answerResults = new List<bool>();

                    foreach (var answer in question.QuestionTemplate.Answers)
                    {
                        var providedAnswer = model.Answers.FirstOrDefault(x => x.Id == answer.Id);
                        answerResults.Add(providedAnswer.IsSelected == answer.IsCorrect);
                    }

                    return answerResults.All(x => x);
            }

            return false;
        }

        private TestTemplateModel MapTestTemplate(TestTemplate template)
        {
            return new TestTemplateModel
            {
                Id = template.Id,
                Name = template.Name,
                Description = template.Description,
                CountOfQuestions = template.CountOfQuestions,
                CreatedAt = template.CreatedAt,
                Questions = template.Questions.Select(x => new QuestionTemplateModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ControlType = x.ControlType,
                    Answers = x.Answers.Select(f => new AnswerTemplateModel
                    {
                        Id = f.Id,
                        IsCorrect = f.IsCorrect,
                        Answer = f.Answer
                    }).ToList()
                }).ToList()
            };
        }

        private KnowledgeTestResultModel GetTestResult(KnowledgeTest test)
        {
            var result = new KnowledgeTestResultModel
            {
                Name = test.TestTemplate.Name,
                CorrectAnswers = test.Questions.Count(x => x.IsProvidedCorrectAnswer),
                IncorrectAnswers = test.Questions.Count(x => !x.IsProvidedCorrectAnswer),
                SubmittedAt = test.ModifiedAt,
            };

            result.Result = Math.Round((result.CorrectAnswers * 100d) / (result.CorrectAnswers + result.IncorrectAnswers), 2);
            result.Mark = (int) Math.Round(result.Result / 10d, 0);

            return result;
        }
    }
}
 