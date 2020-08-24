using VPX.ApiModels;
using VPX.Domain;

namespace VPX.BusinessLogic.Core.Contracts.KnowledgeTests
{
    public interface IKnowledgeTestResultService
    {
        bool IsProvidedCorrectAnswer(KnowledgeTestQuestion question, KnowledgeQuestionModel model);
        KnowledgeTestResultModel GetResult(KnowledgeTest test);
    }
}
