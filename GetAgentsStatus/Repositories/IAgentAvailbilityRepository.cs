using GetAgentsStatus.Entities;
using System.Collections.Generic;

namespace GetAgentsStatus.Repositories
{
    public interface IAgentAvailbilityRepository
    {
        void AddAgentToDatabase(IList<WdrAgentAvailability> extensionQueueInfos);
        void DeactivateOldActivity();
    }
}