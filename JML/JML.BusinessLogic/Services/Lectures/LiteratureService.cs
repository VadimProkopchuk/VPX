using JML.ApiModels;
using JML.BusinessLogic.Core.Contracts.Lectures;
using JML.BusinessLogic.Mappings.Lectures;
using JML.DataAccess.Core.Contracts;
using JML.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Services.Lectures
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
