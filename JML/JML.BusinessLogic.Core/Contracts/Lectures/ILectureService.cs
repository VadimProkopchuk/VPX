using JML.ApiModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Core.Contracts.Lectures
{
    public interface ILectureService
    {
        Task<List<SectionGroupModel>> GetAll();
        Task<LectureModel> GetAsync(string url);
        Task<List<LectureModel>> GetAsync();
        Task<LectureModel> CreateAsync(LectureModel model);
        Task<LectureModel> UpdateAsync(LectureModel model);
        Task<LectureModel> RemoveAsync(Guid id);
    }
}
