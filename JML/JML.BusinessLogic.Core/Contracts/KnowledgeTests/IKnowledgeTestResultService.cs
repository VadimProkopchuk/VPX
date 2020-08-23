using JML.ApiModels;
using JML.Domain;

namespace JML.BusinessLogic.Core.Contracts.KnowledgeTests
{
    public interface IKnowledgeTestResultService
    {
        bool IsProvidedCorrectAnswer(KnowledgeTestQuestion question, KnowledgeQuestionModel model);
        KnowledgeTestResultModel GetResult(KnowledgeTest test);
    }
}
