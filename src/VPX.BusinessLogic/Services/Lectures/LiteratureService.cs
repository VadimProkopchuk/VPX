using System.Threading.Tasks;
using VPX.BusinessLogic.Core.Contracts.Lectures;
using Microsoft.EntityFrameworkCore;
using VPX.ApiModels;
using VPX.BusinessLogic.Mappings.Lectures;
using VPX.DataAccess.Core.Contracts;
using VPX.Domain;

namespace VPX.BusinessLogic.Services.Lectures
{
    public class LiteratureService : ILiteratureService
    {
        private readonly IAppEntityRepository<Literature> literatureRepository;
        private readonly IDataContext dataContext;

        public LiteratureService(IAppEntityRepository<Literature> literatureRepository,
            IDataContext dataContext)
        {
            this.literatureRepository = literatureRepository;
            this.dataContext = dataContext;
        }

        public async Task<LiteratureModel> Get()
        {
            var literature = await literatureRepository.GetQuery()
                .FirstOrDefaultAsync();
            return LiteratureMap.Map(literature);
        }

        public async Task Update(string content)
        {
            var literature = await literatureRepository.GetQuery().FirstOrDefaultAsync();

            if (literature == null)
            {
                literature = new Literature();
                literatureRepository.Add(literature);
            }

            literature.Content = content;
            await dataContext.SaveChangesAsync();
        }
    }
}
