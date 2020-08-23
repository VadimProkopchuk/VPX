using JML.ApiModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Core.Contracts.KnowledgeTests
{
    public interface IKnowledgeTestService
    {
        Task<List<KnowledgeTestResultModel>> Results(Guid id, Guid userId);
    }
}
