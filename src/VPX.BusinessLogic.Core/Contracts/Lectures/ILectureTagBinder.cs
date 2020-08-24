using System.Collections.Generic;
using System.Threading.Tasks;
using VPX.ApiModels;
using VPX.Domain;

namespace VPX.BusinessLogic.Core.Contracts.Lectures
{
    public interface ILectureTagBinder
    {
        Task<Lecture> BindTags(Lecture lecture, List<TagModel> tags);
        Lecture ResetTags(Lecture lecture);
    }
}
