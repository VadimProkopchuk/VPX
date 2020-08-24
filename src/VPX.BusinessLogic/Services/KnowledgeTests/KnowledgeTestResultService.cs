using System;
using System.Collections.Generic;
using System.Linq;
using VPX.BusinessLogic.Core.Contracts.KnowledgeTests;
using VPX.ApiModels;
using VPX.Domain;
using VPX.Enums;

namespace VPX.BusinessLogic.Services.KnowledgeTests
{
    public class KnowledgeTestResultService : IKnowledgeTestResultService
    {
        public KnowledgeTestResultModel GetResult(KnowledgeTest test)
        {
            var result = new KnowledgeTestResultModel
            {
                Name = test.TestTemplate.Name,
                CorrectAnswers = test.Questions.Count(x => x.IsProvidedCorrectAnswer),
                IncorrectAnswers = test.Questions.Count(x => !x.IsProvidedCorrectAnswer),
                SubmittedAt = test.ModifiedAt,
            };

            result.Result = Math.Round((result.CorrectAnswers * 100d) / (result.CorrectAnswers + result.IncorrectAnswers), 2);
            result.Mark = (int)Math.Round(result.Result / 10d, 0);

            return result;
        }

        public bool IsProvidedCorrectAnswer(KnowledgeTestQuestion question, KnowledgeQuestionModel model)
        {
            var correctAnswers = question.QuestionTemplate.Answers.Where(x => x.IsCorrect);

            switch (question.QuestionTemplate.ControlType)
            {
                case ControlType.Text:
                    var correctTextAnswer = correctAnswers.FirstOrDefault();
                    return model.Answers[0].Answer == correctTextAnswer.Answer;

                case ControlType.Single:
                    var correctSingleAnswer = correctAnswers.FirstOrDefault();
                    return model.AnswerId == correctSingleAnswer.Id;

                case ControlType.Multiple:
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
    }
}
