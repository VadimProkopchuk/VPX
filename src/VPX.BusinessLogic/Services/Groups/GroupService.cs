using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPX.BusinessLogic.Core.Contracts.Groups;
using Microsoft.EntityFrameworkCore;
using VPX.ApiModels;
using VPX.BusinessLogic.Mappings.Users;
using VPX.DataAccess.Core.Contracts;
using VPX.Domain;

namespace VPX.BusinessLogic.Services.Groups
{
    public class GroupService : IGroupService
    {
        private readonly IAppEntityRepository<StudyGroup> groupsRepository;
        private readonly IAppEntityRepository<User> usersRepository;
        private readonly IDataContext dataContext;

        public GroupService(IAppEntityRepository<StudyGroup> groupsRepository,
            IAppEntityRepository<User> usersRepository,
            IDataContext dataContext)
        {
            this.groupsRepository = groupsRepository;
            this.usersRepository = usersRepository;
            this.dataContext = dataContext;
        }

        public async Task<GroupModel> Create(string name)
        {
            var group = new StudyGroup { Name = name };
            
            groupsRepository.Add(group);
            await dataContext.SaveChangesAsync();

            return new GroupModel
            {
                Id = group.Id,
                Name = group.Name,
                CreatedAt = group.CreatedAt,
                ModifiedAt = group.ModifiedAt,
                Users = Enumerable.Empty<UserModel>().ToList()
            };
        }

        public async Task<List<GroupModel>> GetAllSimple()
        {
            var groups = await groupsRepository.GetQuery().ToListAsync();

            return groups
                .Select(x => new GroupModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    ModifiedAt = x.ModifiedAt,
                    Users = x.Users.Select(UserMap.Map)
                        .OrderBy(x => x.LastName)
                        .ThenBy(x => x.FirstName)
                        .ToList()
                })
                .ToList();
        }

        public async Task<GroupModel> GetSimple(Guid id)
        {
            var group = await groupsRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == id);

            return new GroupModel
                {
                    Id = group.Id,
                    Name = group.Name,
                    CreatedAt = group.CreatedAt,
                    ModifiedAt = group.ModifiedAt,
                    Users = group.Users.Select(UserMap.Map).ToList()
                };
        }

        public async Task Update(UpdateGroupModel model)
        {
            var group = await groupsRepository.GetQuery().FirstAsync(x => x.Id == model.Id);

            if (group != null)
            {
                group.Name = model.Name;

                foreach (var user in await usersRepository.GetQuery().ToListAsync())
                {
                    user.GroupId = null;
                    if (model.Users != null && model.Users.Contains(user.Id))
                    {
                        user.GroupId = group.Id;
                    }
                }

                await dataContext.SaveChangesAsync();
            }
        }
    }
}
