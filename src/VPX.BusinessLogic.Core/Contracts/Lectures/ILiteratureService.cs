using System.Threading.Tasks;
using VPX.ApiModels;

namespace VPX.BusinessLogic.Core.Contracts.Lectures
{
    public interface ILiteratureService
    {
        Task<LiteratureModel> Get();
        Task Update(string content);
    }
}
