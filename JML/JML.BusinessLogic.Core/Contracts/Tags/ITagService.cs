using JML.ApiModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Core.Contracts.Tags
{
    public interface ITagService
    {
        Task<List<TagModel>> GetAsync();
    }
}
