using JML.ApiModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Core.Contracts.Groups
{
    public interface IGroupService
    {
        Task<List<GroupModel>> GetAllSimple();
        Task<GroupModel> Create(string name);
    }
}
