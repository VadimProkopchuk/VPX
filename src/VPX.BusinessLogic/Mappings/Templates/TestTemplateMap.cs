using System.Linq;
using VPX.ApiModels;
using VPX.Domain.Templates;

namespace VPX.BusinessLogic.Mappings.Templates
{
    internal class TestTemplateMap
    {
        public static TestTemplateModel Map(TestTemplate template)
        {
            if (template == null)
            {
                return null;
            }

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
    }
}
