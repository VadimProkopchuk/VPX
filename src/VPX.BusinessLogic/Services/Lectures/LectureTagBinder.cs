using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPX.BusinessLogic.Core.Contracts.Lectures;
using VPX.ApiModels;
using VPX.BusinessLogic.Mappings.Lectures;
using VPX.DataAccess.Core.Contracts;
using VPX.Domain;

namespace VPX.BusinessLogic.Services.Lectures
{
    public class LectureTagBinder : ILectureTagBinder
    {
        private readonly IAppEntityRepository<LectureTag> lectureTagRepository;
        private readonly IAppEntityRepository<Tag> tagRepository;
        private readonly IDataContext dataContext;

        public LectureTagBinder(IAppEntityRepository<LectureTag> lectureTagRepository,
            IAppEntityRepository<Tag> tagRepository,
            IDataContext dataContext)
        {
            this.lectureTagRepository = lectureTagRepository;
            this.tagRepository = tagRepository;
            this.dataContext = dataContext;
        }

        public async Task<Lecture> BindTags(Lecture lecture, List<TagModel> tags)
        {
            var entityTags = await CreateTagsAsync(tags);
            return await BindTags(lecture, entityTags);
        }

        public Lecture ResetTags(Lecture lecture)
        {
            if (lecture.LectureTags == null)
            {
                lecture.LectureTags = new List<LectureTag>();
            }
            else
            {
                foreach (var lectureTag in lecture.LectureTags)
                {
                    lectureTagRepository.Remove(lectureTag);
                }
                lecture.LectureTags.Clear();
            }

            return lecture;
        }

        private async Task<List<Tag>> CreateTagsAsync(List<TagModel> tagList)
        {
            tagList ??= new List<TagModel>();
            var tags = tagList
                .Where(x => x.Value.HasValue)
                .Select(TagMap.Map)
                .ToList();

            foreach (var tag in tagList.Where(x => !x.Value.HasValue))
            {
                var entityTag = new Tag() { Name = tag.Display };

                tagRepository.Add(entityTag);
                tags.Add(entityTag);
            }

            await dataContext.SaveChangesAsync();

            return tags;
        }

        private async Task<Lecture> BindTags(Lecture lecture, List<Tag> tags)
        {
            lecture = ResetTags(lecture);

            foreach (var tag in tags)
            {
                var lectureTag = new LectureTag
                {
                    LectureId = lecture.Id,
                    TagId = tag.Id
                };

                lecture.LectureTags.Add(lectureTag);
                lectureTagRepository.Add(lectureTag);
            }

            await dataContext.SaveChangesAsync();

            return lecture;
        }
    }
}
