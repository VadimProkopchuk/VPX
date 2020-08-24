using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPX.BusinessLogic.Core.Contracts.KnowledgeTests;
using Microsoft.EntityFrameworkCore;
using VPX.ApiModels;
using VPX.DataAccess.Core.Contracts;
using VPX.Domain;

namespace VPX.BusinessLogic.Services.KnowledgeTests
{
    public class KnowledgeTestService : IKnowledgeTestService
    {
        private readonly IAppEntityRepository<KnowledgeTest> knowledgeTestRepository;
        private readonly IKnowledgeTestResultService knowledgeTestResultService;

        public KnowledgeTestService(IAppEntityRepository<KnowledgeTest> knowledgeTestRepository,
            IKnowledgeTestResultService knowledgeTestResultService)
        {
            this.knowledgeTestRepository = knowledgeTestRepository;
            this.knowledgeTestResultService = knowledgeTestResultService;
        }

        public async Task<List<KnowledgeTestResultModel>> Results(Guid id, Guid userId)
        {
            var results = await knowledgeTestRepository
                .GetQuery()
                .Where(x => x.TestTemplateId == id && x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return results.Select(knowledgeTestResultService.GetResult).ToList();
        }
    }
}
