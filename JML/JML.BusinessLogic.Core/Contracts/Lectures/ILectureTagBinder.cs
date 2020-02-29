using JML.ApiModels;
using JML.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Core.Contracts.Lectures
{
    public interface ILectureTagBinder
    {
        Task<Lecture> BindTags(Lecture lecture, List<TagModel> tags);
    }
}
