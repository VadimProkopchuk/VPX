using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPX.BusinessLogic.Core.Contracts.Tags;
using Microsoft.EntityFrameworkCore;
using VPX.ApiModels;
using VPX.BusinessLogic.Mappings.Lectures;
using VPX.DataAccess.Core.Contracts;
using VPX.Domain;

namespace VPX.BusinessLogic.Services.Tags
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
