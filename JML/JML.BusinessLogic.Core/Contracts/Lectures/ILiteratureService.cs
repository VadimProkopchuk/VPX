using JML.ApiModels;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Core.Contracts.Lectures
{
    public interface ILiteratureService
    {
        Task<LiteratureModel> Get();
        Task Update(string content);
    }
}
