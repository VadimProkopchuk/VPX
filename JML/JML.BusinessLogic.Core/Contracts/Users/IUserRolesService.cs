using JML.ApiModels;
using JML.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Core.Contracts.Users
{
    public interface IUserRolesService
    {
        Task<UserProfileModel> AddRole(Guid userId, Role role);
        Task<UserProfileModel> RemoveRole(Guid userId, Role role);
    }
}
