using System;
using System.Threading.Tasks;
using VPX.ApiModels;
using VPX.Enums;

namespace VPX.BusinessLogic.Core.Contracts.Users
{
    public interface IUserRolesService
    {
        Task<UserProfileModel> AddRole(Guid userId, Role role);
        Task<UserProfileModel> RemoveRole(Guid userId, Role role);
    }
}
