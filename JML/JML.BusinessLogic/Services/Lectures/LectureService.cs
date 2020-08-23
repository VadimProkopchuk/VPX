using JML.ApiModels;
using JML.BusinessLogic.Core.Contracts.Lectures;
using JML.BusinessLogic.Mappings.Lectures;
using JML.DataAccess.Core.Contracts;
using JML.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Services.Lectures
{
    public class LectureService : ILectureService
    {
        private readonly IAppEntityRepository<Lecture> lectureRepository;
        private readonly ILectureTagBinder lectureTagBinder;
        private readonly IDataContext dataContext;

        public LectureService(IAppEntityRepository<Lecture> lectureRepository,
            ILectureTagBinder lectureTagBinder,
            IDataContext dataContext)
        {
            this.lectureRepository = lectureRepository;
            this.lectureTagBinder = lectureTagBinder;
            this.dataContext = dataContext;
        }

        public async Task<LectureModel> CreateAsync(LectureModel model)
        {
            var lecture = new Lecture
            {
                Name = model.Name,
                Url = model.Url,
                Content = model.Content,
                Preview = model.Preview,
                Section = model.Section,
            };

            lectureRepository.Add(lecture);

            await dataContext.SaveChangesAsync();

            lecture = await lectureTagBinder.BindTags(lecture, model.Tags);

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

        public async Task<LectureModel> UpdateAsync(LectureModel model)
        {
            var lecture = await lectureRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == model.Id);

            if (lecture == null)
            {
                throw new ApplicationException("Объект не найден.");
            }

            lecture.Name = model.Name;
            lecture.Preview = model.Preview;
            lecture.Url = model.Url;
            lecture.Content = model.Content;
            lecture.Section = model.Section;

            await dataContext.SaveChangesAsync();

            lecture = await lectureTagBinder.BindTags(lecture, model.Tags);

            return LectureMap.Map(lecture);
        }

        public async Task<LectureModel> RemoveAsync(Guid id)
        {
            var lecture = await lectureRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (lecture == null)
            {
                throw new ApplicationException("Объект не найден.");
            }

            lectureTagBinder.ResetTags(lecture);
            lectureRepository.Remove(lecture);

            await dataContext.SaveChangesAsync();

            return LectureMap.Map(lecture);
        }

        public async Task<List<SectionGroupModel>> GetAll()
        {
            var lectures = await GetAsync();
            var groups = lectures
                .GroupBy(x => x.Section)
                .Select(x => new SectionGroupModel
                {
                    Section = x.Key,
                    Lections = x.ToList()
                })
                .OrderBy(x => x.Section)
                .ToList();

            return groups;
        }
    }
}
