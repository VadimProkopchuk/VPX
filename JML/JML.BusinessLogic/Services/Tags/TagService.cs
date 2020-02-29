using JML.ApiModels;
using JML.BusinessLogic.Core.Contracts.Tags;
using JML.BusinessLogic.Mappings.Lectures;
using JML.DataAccess.Core.Contracts;
using JML.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Services.Tags
{
    public class TagService : ITagService
    {
        private readonly IAppEntityRepository<Tag> tagRepository;

        public TagService(IAppEntityRepository<Tag> tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public async Task<List<TagModel>> GetAsync()
        {
            var tags = await tagRepository.GetQuery().ToListAsync();
            return tags.Select(TagMap.Map).ToList();
        }
    }
}
