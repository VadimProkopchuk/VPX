using JML.ApiModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Core.Contracts.Groups
{
    public interface IGroupService
    {
        Task<GroupModel> GetSimple(Guid id);
        Task<List<GroupModel>> GetAllSimple();
        Task<GroupModel> Create(string name);
        Task Update(UpdateGroupModel model);
    }
}
