using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VPX.ApiModels;

namespace VPX.BusinessLogic.Core.Contracts.KnowledgeTests
{
    public interface IKnowledgeTestService
    {
        Task<List<KnowledgeTestResultModel>> Results(Guid id, Guid userId);
    }
}
