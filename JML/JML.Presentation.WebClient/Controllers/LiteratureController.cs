using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JML.ApiModels;
using JML.DataAccess.Core.Contracts;
using JML.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JML.Presentation.WebClient.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LiteratureController : ControllerBase
    {
        private readonly IAppEntityRepository<Literature> literatureRepository;
        private readonly IDataContext dataContext;

        public LiteratureController(IAppEntityRepository<Literature> literatureRepository,
            IDataContext dataContext)
        {
            this.literatureRepository = literatureRepository;
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var literature = await literatureRepository.GetQuery().FirstOrDefaultAsync();
            var model = new LiteratureModel
            {
                Content = literature?.Content ?? String.Empty,
                ModifiedAt = literature?.ModifiedAt ?? DateTime.Now,
            };

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Post(LiteratureModel model)
        {
            var literature = await literatureRepository.GetQuery().FirstOrDefaultAsync();

            if (literature == null)
            {
                literature = new Literature();
                literatureRepository.Add(literature);
            }

            literature.Content = model.Content;
            await dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}