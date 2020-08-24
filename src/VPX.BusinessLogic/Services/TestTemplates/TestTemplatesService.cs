using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPX.BusinessLogic.Core.Contracts.Accounts;
using VPX.BusinessLogic.Core.Contracts.KnowledgeTests;
using VPX.BusinessLogic.Core.Contracts.TestTemplates;
using Microsoft.EntityFrameworkCore;
using VPX.ApiModels;
using VPX.BusinessLogic.Mappings.Templates;
using VPX.DataAccess.Core.Contracts;
using VPX.Domain;
using VPX.Domain.Templates;
using VPX.Enums;

namespace VPX.BusinessLogic.Services.TestTemplates
{
    public class TestTemplatesService : ITestTemplatesService
    {
        private readonly IKnowledgeTestResultService knowledgeTestResultService;
        private readonly IAppEntityRepository<TestTemplate> testTemplateRepository;
        private readonly IAppEntityRepository<KnowledgeTest> knowledgeTestRepository;
        private readonly ICurrentUser currentUser;
        private readonly IDataContext dataContext;

        public TestTemplatesService(IKnowledgeTestResultService knowledgeTestResultService,
            IAppEntityRepository<TestTemplate> testTemplateRepository,
            IAppEntityRepository<KnowledgeTest> knowledgeTestRepository,
            ICurrentUser currentUser,
            IDataContext dataContext)
        {
            this.knowledgeTestResultService = knowledgeTestResultService;
            this.testTemplateRepository = testTemplateRepository;
            this.knowledgeTestRepository = knowledgeTestRepository;
            this.currentUser = currentUser;
            this.dataContext = dataContext;
        }

        public async Task<TestTemplateModel> Create(TestTemplateModel model)
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

            return TestTemplateMap.Map(testTemplate);
        }

        public async Task<DeleteTestModel> Delete(DeleteTestModel model)
        {
            var template = await testTemplateRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == model.Id);

            template.IsActive = false;
            await dataContext.SaveChangesAsync();

            return model;
        }

        public async Task<KnowledgeTestModel> Execute(ExecuteTestModel model)
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

            return new KnowledgeTestModel
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
                        Answer = x.QuestionTemplate.ControlType == ControlType.Text ? null : f.Answer
                    }).ToList()
                }).ToList()
            };
        }

        public async Task<List<CardTestTemplateModel>> GetCardTemplates()
        {
            var templates = await testTemplateRepository
                .GetQuery()
                .Where(x => x.IsActive)
                .ToListAsync();
            var user = await currentUser.GetCurrentUserAsync();
            var userTests = (await Queryable.Where(knowledgeTestRepository
                    .GetQuery(), x => x.UserId == user.Id)
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

            return templateCards;
        }

        public async Task<KnowledgeTestResultModel> Submit(KnowledgeTestModel model)
        {
            var test = await knowledgeTestRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == model.Id);

            test.ModifiedAt = DateTime.Now;

            foreach (var question in model.Questions)
            {
                var knowledgeQuestion = test.Questions.FirstOrDefault(x => x.Id == question.Id);

                knowledgeQuestion.IsProvidedCorrectAnswer = knowledgeTestResultService.IsProvidedCorrectAnswer(knowledgeQuestion, question);
            }

            await dataContext.SaveChangesAsync();

            return knowledgeTestResultService.GetResult(test);
        }
    }
}
