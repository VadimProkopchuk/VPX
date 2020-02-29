using JML.ApiModels;
using JML.BusinessLogic.Core.Contracts.Lectures;
using JML.BusinessLogic.Mappings.Lectures;
using JML.DataAccess.Core.Contracts;
using JML.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Services.Lectures
{
    public class LectureService : ILectureService
    {
        private readonly IAppEntityRepository<Lecture> lectureRepository;
        private readonly IAppEntityRepository<Tag> tagRepository;
        private readonly IAppEntityRepository<LectureTag> lectureTagRepository;
        private readonly IDataContext dataContext;

        public LectureService(IAppEntityRepository<Lecture> lectureRepository,
            IAppEntityRepository<Tag> tagRepository,
            IAppEntityRepository<LectureTag> lectureTagRepository,
            IDataContext dataContext)
        {
            this.lectureRepository = lectureRepository;
            this.tagRepository = tagRepository;
            this.lectureTagRepository = lectureTagRepository;
            this.dataContext = dataContext;
        }

        public async Task<LectureModel> CreateAsync(LectureModel model)
        {
            var lecture = new Lecture
            {
                Name = model.Name,
                Url = model.Url,
                Content = model.Content,
                Preview = model.Preview
            };

            lectureRepository.Add(lecture);

            await dataContext.SaveChangesAsync();

            lecture = await BindTags(lecture, await GetTags(model.Tags));

            return LectureMap.Map(lecture);
        }

        public async Task<List<LectureModel>> GetAsync()
        {
            var lectures = await lectureRepository.GetQuery().ToListAsync();
            return lectures.Select(LectureMap.Map).ToList();
        }

        public async Task<LectureModel> GetAsync(string url)
        {
            var lecture = await lectureRepository.GetQuery().FirstOrDefaultAsync(x => x.Url == url);
            return LectureMap.Map(lecture);
        }

        // todo: refactor
        private async Task<List<Tag>> GetTags(List<TagModel> tagList)
        {
            tagList = tagList ?? new List<TagModel>();

            var tags = (tagList ?? new List<TagModel>())
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


        // todo: refactor
        private async Task<Lecture> BindTags(Lecture lecture, List<Tag> tags)
        {
            if (lecture.LectureTags == null)
            {
                lecture.LectureTags = new List<LectureTag>();
            }

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
