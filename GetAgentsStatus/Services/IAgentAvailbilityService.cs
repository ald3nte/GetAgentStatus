using GetAgentsStatus.Entities;
using System.Collections.Generic;

namespace GetAgentsStatus.Services
{
    public interface IAgentAvailbilityService
    {
        void AddAgentsAvailabilityToDatabase(IEnumerable<WdrAgentAvailability> extensionQueueInfos);
        IEnumerable<WdrAgentAvailability> GetAgentsAvailabilityList();

        void DeactivateOldAgentAvailability();
    }
}