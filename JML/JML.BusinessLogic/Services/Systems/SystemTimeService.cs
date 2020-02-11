using JML.BusinessLogic.Core.Contracts.Systems;
using System;

namespace JML.BusinessLogic.Services.Systems
{
    public class SystemTimeService : ISystemTimeService
    {
        public DateTime GetDateUtc() => DateTime.UtcNow;
    }
}
