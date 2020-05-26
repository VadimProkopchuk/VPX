using JML.ApiModels;
using JML.Domain;
using System.Linq;

namespace JML.BusinessLogic.Mappings.Lectures
{
    internal class LectureMap
    {
        public static LectureModel Map(Lecture lecture)
        {
            if (lecture == null)
            {
                return null;
            }

            return new LectureModel
            {
                Id = lecture.Id,
                Url = lecture.Url,
                Name = lecture.Name,
                Content = lecture.Content,
                CreatedAt = lecture.CreatedAt,
                ModifiedAt = lecture.ModifiedAt,
                Preview = lecture.Preview,
                Section = lecture.Section,
                Tags = lecture?.LectureTags.Select(x => TagMap.Map(x.Tag)).ToList()
            };
        }
    }
}
