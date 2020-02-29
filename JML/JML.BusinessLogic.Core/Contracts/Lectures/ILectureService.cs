using JML.ApiModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Core.Contracts.Lectures
{
    public interface ILectureService
    {
        Task<LectureModel> GetAsync(string url);
        Task<List<LectureModel>> GetAsync();
        Task<LectureModel> CreateAsync(LectureModel model);
        Task<LectureModel> UpdateAsync(LectureModel model);
    }
}
