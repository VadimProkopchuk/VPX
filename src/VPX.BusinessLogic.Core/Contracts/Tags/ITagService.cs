using System.Collections.Generic;
using System.Threading.Tasks;
using VPX.ApiModels;

namespace VPX.BusinessLogic.Core.Contracts.Tags
{
    public interface ITagService
    {
        Task<List<TagModel>> GetAsync();
    }
}
