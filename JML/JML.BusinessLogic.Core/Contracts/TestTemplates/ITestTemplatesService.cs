using JML.ApiModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Core.Contracts.TestTemplates
{
    public interface ITestTemplatesService
    {
        Task<List<CardTestTemplateModel>> GetCardTemplates();
        Task<TestTemplateModel> Create(TestTemplateModel model);
        Task<KnowledgeTestModel> Execute(ExecuteTestModel model);
        Task<KnowledgeTestResultModel> Submit(KnowledgeTestModel model);
        Task<DeleteTestModel> Delete(DeleteTestModel model);
    }
}
