using System;
using VPX.BusinessLogic.Core.Contracts.Systems;

namespace VPX.BusinessLogic.Services.Systems
{
    public class SystemTimeService : ISystemTimeService
    {
        public DateTime GetDateUtc() => DateTime.UtcNow;
    }
}
