using JML.ApiModels;
using JML.BusinessLogic.Core.Contracts.Groups;
using JML.BusinessLogic.Mappings.Users;
using JML.DataAccess.Core.Contracts;
using JML.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Services.Groups
{
    public class GroupService : IGroupService
    {
        private readonly IAppEntityRepository<StudyGroup> groupsRepository;
        private readonly IDataContext dataContext;

        public GroupService(IAppEntityRepository<StudyGroup> groupsRepository,
            IDataContext dataContext)
        {
            this.groupsRepository = groupsRepository;
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
    }
}
