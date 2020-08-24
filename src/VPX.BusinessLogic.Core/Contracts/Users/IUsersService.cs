using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VPX.ApiModels;
using VPX.Domain;

namespace VPX.BusinessLogic.Core.Contracts.Users
{
    public interface IUsersService
    {
        Task<User> GetByEmailAsync(string email);
        Task<bool> HasUserByEmailAsync(string email);
        Task<List<UserModel>> GetTeachers();
        Task<UserProfileModel> GetProfile(Guid id);
        Task<UserModel> Update(UserUpdatesModel model);
        Task<UserProfileModel> Lock(Guid userId);
        Task<UserProfileModel> Unlock(Guid userId);
        Task<List<UserWithoutGroupModel>> GetUsersWithoutGroups();
    }
}
