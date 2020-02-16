using JML.BusinessLogic.Core.Contracts.Systems;
using Microsoft.AspNetCore.Http;
using System;

namespace JML.Presentation.WebClient.Infrastructure.Context
{
    public class HttpContextService : IContextService
    {
        private readonly IHttpContextAccessor contextAccessor;

        public HttpContextService(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public Guid? GetCurrentUserId()
        {
            var userId = contextAccessor.HttpContext.User.Identity.Name;

            if (Guid.TryParse(userId, out var id))
            {
                return id;
            }

            return null;
        }
    }
}
